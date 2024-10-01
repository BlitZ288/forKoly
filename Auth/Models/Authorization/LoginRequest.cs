using System.ComponentModel.DataAnnotations;

namespace Auth.Models.Authorization
{
    /// <summary>
    /// Модель запроса на вход в систему
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// Логин.
        /// </summary>
        [Required]
        public required string UserName { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required]
        public required string Password { get; set; }
    }
}
