namespace Auth.Application.Models.Authorization
{
    /// <summary>
    /// Транспортная модель запроса на регистрацию 
    /// </summary>
    public class RegisterRequestDto
    {
        /// <summary>
        /// Наименование пользователя.
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        public required string Password { get; set; }

        /// <summary>
        /// Почта.
        /// </summary>
        public string? Email { get; set; }
    }
}
