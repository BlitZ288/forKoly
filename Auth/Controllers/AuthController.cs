using Asp.Versioning;
using Auth.Application.Models.Authorization;
using Auth.Application.Services;
using Auth.Models.Authorization;
using Auth.Models.User;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Auth.Controllers
{
    [ApiVersion(1.0)]
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthorizationService _authorizationService;

        private readonly IMapper _mapper;

        public AuthController(AuthorizationService userService, IMapper mapper)
        {
            _authorizationService = userService;
            _mapper = mapper;
        }

        /// <summary>
        /// Вход в систему.
        /// </summary>
        /// <param name="request">Модель запроса на вход в систему.</param>
        /// <returns>Токен авторизации и токен обновления.</returns>
        /// <response code="200">Пользователь успешно авторизовался.</response>       
        /// <response code="400">Валидационная ошибка.</response>       
        /// <response code="404">Пользователь не найден в системе.</response> 
        /// <response code="500">Непредвиденная ошибка сервера.</response> 
        /// <remarks>
        /// Базовый пример: 
        /// 
        ///     Post /login
        ///     {
        ///         "userName":"UserTest",
        ///         "password":"X_hlT49j8B"
        ///     }
        ///     
        /// </remarks>
        [HttpPost("login")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                var userModel = _mapper.Map<LoginRequestDto>(request);

                var authorizationModel = await _authorizationService.Login(userModel);

                var result = _mapper.Map<LoginResponse>(authorizationModel);

                return Ok(result);
            }
            catch (InvalidOperationException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

        }

        /// <summary>
        /// Регистрация пользователя.
        /// </summary>
        /// <param name="request">Модель запроса на регистрацию в систему.</param>
        /// <returns>Идентификатор нового пользователя.</returns>
        /// <response code="200">Пользователь успешно зарегистрирован.</response>       
        /// <response code="409">Пользователь с таким именем уже существует.</response>  
        /// <response code="400">Непредвиденная ошибка сервера.</response> 
        /// <remarks>
        /// Базовый пример: 
        /// 
        ///     Post /register
        ///     {
        ///         "userName":"UserTest",
        ///         "password":"X_hlT49j8B",
        ///         "email":"test@examole.ru"
        ///     }
        ///     
        /// </remarks>
        [HttpPost("register")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            try
            {
                var userModel = _mapper.Map<RegisterRequestDto>(request);

                var userId = await _authorizationService.Register(userModel);

                return Ok(userId);
            }
            catch (ArgumentException)
            {
                return Conflict();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Обновление авторизованного токена по токену обновления
        /// </summary>
        /// <param name="refreshToken">Токен обновления</param>
        /// <returns>Новая пара токенов</returns>
        /// <response code="200">Токен успешно обновлен.</response>       
        /// <response code="409">Токен обновления пустой.</response>       
        /// <response code="400">Непредвиденная ошибка.</response>       
        [HttpPost("refresh")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Conflict)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Refresh(string refreshToken)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(refreshToken))
                {
                    return Conflict("Токен обновления пустой");
                }

                var userId = User.FindFirstValue("uid");
                if (userId == null)
                {
                    return BadRequest();
                }
                var result = await _authorizationService.Refresh(refreshToken, long.Parse(userId));

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                //var uidClaims = User.FindFirstValue("uid");

                //var isClaimValid = uidClaims == null || !long.TryParse(uidClaims, out var userId);

                //if (!isClaimValid)
                //{
                //    return BadRequest();
                //}

                //await _authorizationService.Logout(userId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        ///TODO: 
        /// 1.Разработать нормальную систему ошибок.
        ///2. Сделать выход из системы 
        ///2.1 Подключить Redis
    }
}
