namespace Auth.Domain.Entities
{
    /// <summary>
    /// Сессии в приложении.
    /// </summary>
    public class Session
    {
        /// <summary>
        /// Идентификатор сессии.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя.
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// Токен авторизации
        /// </summary>
        public required string AccessToken { get; set; }

        /// <summary>
        /// Токен обновления.
        /// </summary>
        public required string RefreshToken { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Дата истечения токена.
        /// </summary>
        public DateTime ExpiresDate { get; set; }

        /// <summary>
        /// Создание сессий авторизации.
        /// </summary>
        /// <param name="userId">Идентификатор пользователя.</param>
        /// <param name="refreshToken">Токен обновления.</param>
        /// <returns>Новая модель пользователя.</returns>
        public static Session Create(long userId, string refreshToken, string accessToken)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(refreshToken);
            ArgumentException.ThrowIfNullOrWhiteSpace(accessToken);

            return new Session
            {
                UserId = userId,
                RefreshToken = refreshToken,
                AccessToken = accessToken,
                ExpiresDate = DateTime.UtcNow.AddMinutes(10),
                CreatedDate = DateTime.UtcNow
            };
        }

    }
}
