using System.ComponentModel.DataAnnotations;

namespace Auth.Models.User
{
    /// <summary>
    /// Запрос на регистрацию пользователя.
    /// </summary>
    public class RegisterRequest
    {
        /// <summary>
        /// Наименование пользователя.
        /// </summary>
        [Required]
        public required string UserName { get; set; }

        /// <summary>
        /// Пароль.
        /// </summary>
        [Required]
        public required string Password { get; set; }

        /// <summary>
        /// Почта.
        /// </summary>
        public string? Email { get; set; }
    }
}
