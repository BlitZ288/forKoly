using Auth.Application.ExternalServices;
using Auth.Common.Model.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Auth.Application.Services
{
    /// <summary>
    /// Сервис для работы с токенами.
    /// </summary>
    public class TokenService
    {
        /// <summary>
        /// Конфигурации для генерации токена.
        /// </summary>
        private readonly JWTConfigurationOption _jWTConfiguration;

        private readonly ISessionRepository _sessionRepository;

        private readonly IUserRepository _userRepository;

        public TokenService(IOptions<JWTConfigurationOption> options,
            IUserRepository userRepository,
            ISessionRepository sessionRepository)
        {
            _jWTConfiguration = options.Value;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
        }

        /// <summary>
        /// Метод генерации JWT токена.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Модель сгенерированный токена</returns>
        public async Task<JwtSecurityToken> GenerateJwtToken(long userId)
        {
            var user = await _userRepository.GetUserById(userId);
            if (user == null)
            {
                throw new InvalidOperationException($"Пользователь с id:{userId} не найден");
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Name, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", userId.ToString()),
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTConfiguration.SecretKey));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
             issuer: _jWTConfiguration.Issuer,
             audience: _jWTConfiguration.Audience,
             claims: claims,
             expires: DateTime.UtcNow.AddMinutes(_jWTConfiguration.DurationInMinutes),
             signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

        /// <summary>
        /// Проверить существует ли сессия.
        /// </summary>
        /// <param name="accessToken">Токен авторизации.</param>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <returns>Флаг проверки.</returns>
        public async Task<bool> IsExistSession(string accessToken, long userId)
        {
            var session = await _sessionRepository.GetSessionByAccessToken(accessToken);
            if (session == null || session.UserId != userId)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Создание токена обновления.
        /// </summary>
        /// <returns>Токен обновления</returns>
        public string GenerateRefreshToken()
        {
            var random = new byte[32];

            var generator = RandomNumberGenerator.GetBytes(128 / 8);

            return Convert.ToBase64String(generator);
        }
    }
}
