namespace Auth.Application.Models.User
{
    public class UserModel
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; } = string.Empty;

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
