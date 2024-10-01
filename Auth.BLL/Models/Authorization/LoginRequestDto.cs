namespace Auth.Application.Models.Authorization
{
    /// <summary>
    /// Транспортирная модель запроса на вход в систему 
    /// </summary>
    public class LoginRequestDto
    {
        /// <summary>
        /// Наименование.
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// Почта.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Пароль.
        /// </summary>
        public string Password { get; set; } = string.Empty;
    }
}
