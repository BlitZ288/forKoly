using Auth.Domain.Entities;

namespace Auth.Application.ExternalServices
{
    /// <summary>
    /// Репозиторий для работы с сессиями приложения
    /// </summary>
    public interface ISessionRepository
    {
        /// <summary>
        /// Создания сессия
        /// </summary>
        /// <param name="session">Модель сессии</param>
        /// <returns>Новая модель пользователя</returns>
        public Task<Session> Create(Session session);

        /// <summary>
        /// Получить модель сессии по идентификатору пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Модель сессии</returns>
        public Task<Session?> GetSessionByUserId(long userId);

        /// <summary>
        /// Получить модель сессии по токену авторизации.
        /// </summary>
        /// <param name="accessToken">Токен авторизации.</param>
        /// <returns>Модель сессии</returns>
        public Task<Session?> GetSessionByAccessToken(string accessToken);

        /// <summary>
        /// Попытка получение сессию по токену обновления.
        /// </summary>
        /// <param name="refreshToken">Токен обновления.</param>
        /// <returns>Модель сессии</returns>
        public Task<Session?> GetSessionByRefreshToken(string refreshToken);

        /// <summary>
        /// Обновление модель сессии
        /// </summary>
        /// <param name="session">Модель сессии</param>>
        public Task UpdateSession(Session session);

        /// <summary>
        /// Удаление сессии.
        /// </summary>
        /// <param name="session">Модель сессии</param>
        public Task DeleteSession(Session session);
    }
}
