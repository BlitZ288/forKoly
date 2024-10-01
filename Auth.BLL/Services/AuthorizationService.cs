using Auth.Application.ExternalServices;
using Auth.Application.Models.Authorization;
using Auth.Common.Tools;
using Auth.Domain.Entities;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;

namespace Auth.Application.Services
{
    /// <summary>
    /// Сервис авторизации.
    /// </summary>
    public class AuthorizationService
    {
        private readonly IUserRepository _userRepository;

        private readonly ISessionRepository _sessionRepository;

        private readonly TokenService _tokenService;

        private readonly ILogger<AuthorizationService> _logger;

        public AuthorizationService(
            IUserRepository userRepository,
            ILogger<AuthorizationService> logger,
            TokenService tokenService,
            ISessionRepository sessionRepository)
        {
            _userRepository = userRepository;
            _logger = logger;
            _tokenService = tokenService;
            _sessionRepository = sessionRepository;
        }

        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="registerRequest">Модель запроса на регистрацию .</param>
        /// <returns>Идентификатор нового пользователя.</returns>
        /// <exception cref="ArgumentException">Неликвидные данные.</exception>
        public async Task<long> Register(RegisterRequestDto registerRequest)
        {
            var (passwordHash, salt) = GenerateHashPassword.GetGenerateHashPasswordWithSalt(registerRequest.Password);

            var isExists = await _userRepository.IsExistUserByName(registerRequest.UserName);
            if (isExists)
            {
                _logger.LogError("Пользователь с таким именем уже существует", registerRequest.UserName);
                throw new ArgumentException("Пользователь с таким именем уже существует");
            }

            var user = User.Create(registerRequest.UserName, passwordHash, registerRequest.Email, salt);

            var newUser = await _userRepository.CreateUser(user);

            return newUser.Id;
        }

        /// <summary>
        /// Вход пользователя.
        /// </summary>
        /// <param name="loginRequestDto">Модель пользователя.</param>
        /// <returns>Access токен пользователя.</returns>
        /// <exception cref="ArgumentException">Неликвидные данные</exception>
        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto)
        {
            var user = await _userRepository.GetUserByName(loginRequestDto.UserName);
            if (user == null)
            {
                _logger.LogError("Пользователь с таким именем не найден", loginRequestDto.UserName);
                throw new InvalidOperationException("Пользователь с таким именем не найден");
            }

            var isValidPassword = GenerateHashPassword.GetPasswordHash(loginRequestDto.Password, user.Salt) == user.PasswordHash;
            if (!isValidPassword)
            {
                _logger.LogError($"Пароль для учетной записи {loginRequestDto.UserName} неверен", loginRequestDto);
                throw new ArgumentException($"Пароль для учетной записи {loginRequestDto.UserName} неверен");
            }

            var accesToken = await _tokenService.GenerateJwtToken(user.Id);
            var refreshToken = _tokenService.GenerateRefreshToken();

            var accesTokenString = new JwtSecurityTokenHandler().WriteToken(accesToken);

            var session = Session.Create(user.Id, refreshToken, accesTokenString);

            await _sessionRepository.Create(session);

            var result = new LoginResponseDto
            {
                AccessToken = accesTokenString,
                RefreshToken = refreshToken
            };

            return result;
        }

        /// <summary>
        /// Обновление пар токенов.
        /// </summary>
        /// <param name="refreshToken">Токен обновления.</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Модель авторизации.</returns>
        public async Task<LoginResponseDto> Refresh(string refreshToken, long userId)
        {
            var currentSession = await _sessionRepository.GetSessionByRefreshToken(refreshToken);
            if (currentSession == null)
            {
                throw new InvalidOperationException("Текущая сессия не найдена");
            }

            var isRefreshTonenValid = (currentSession.UserId == userId);
            if (!isRefreshTonenValid)
            {
                _logger.LogError("Данные для обновление токена невалидные");
                throw new InvalidOperationException("Данные для обновление токена невалидные");
            }

            var accesToken = await _tokenService.GenerateJwtToken(userId);
            var newRefreshToken = _tokenService.GenerateRefreshToken();
            var accesTokenString = new JwtSecurityTokenHandler().WriteToken(accesToken);

            var session = Session.Create(userId, newRefreshToken, accesTokenString);

            await _sessionRepository.Create(session);
            await _sessionRepository.DeleteSession(currentSession);

            var result = new LoginResponseDto
            {
                AccessToken = accesTokenString,
                RefreshToken = newRefreshToken
            };

            return result;
        }

        /// <summary>
        /// Выход из системы.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <exception cref="InvalidOperationException">Ошибка если сессия не была найдена.</exception>
        public async Task Logout(long userId)
        {
            var session = await _sessionRepository.GetSessionByUserId(userId);

            if (session == null)
            {
                _logger.LogError("Сессия для пользователя не найдена");
                throw new InvalidOperationException();
            }

            await _sessionRepository.DeleteSession(session);
        }
    }
}