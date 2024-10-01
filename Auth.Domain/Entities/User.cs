namespace Auth.Domain.Entities
{
    /// <summary>
    /// Модель пользователя.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Почта.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Hash пароля.
        /// </summary>
        public required string PasswordHash { get; set; }

        /// <summary>
        /// Соль для пароля.
        /// </summary>
        public string? Salt { get; set; }

        /// <summary>
        /// Создание пользователя.
        /// </summary>
        /// <param name="name">Наименование.</param>
        /// <param name="passwordHash">Хеш-пароля.</param>
        /// <param name="email">Почта.</param>
        /// <param name="salt">Соль для пароля.</param>
        /// <returns>Модель пользователя.</returns>
        public static User Create(string name, string passwordHash, string? email, string? salt)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(name);
            ArgumentException.ThrowIfNullOrWhiteSpace(passwordHash);

            return new User
            {
                Name = name,
                PasswordHash = passwordHash,
                Email = email,
                Salt = salt
            };
        }
    }
}
