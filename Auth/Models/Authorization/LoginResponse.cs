using System.ComponentModel.DataAnnotations;

namespace Auth.Models.Authorization
{
    /// <summary>
    /// Модель ответа на успешный вход в систему.
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Токен доступа.
        /// </summary>
        [Required]
        public required string AccessToken { get; set; }

        /// <summary>
        /// Токен обновления.
        /// </summary>
        [Required]
        public required string RefreshToken { get; set; }
    }
}
