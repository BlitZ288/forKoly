using Auth.Domain.Entities;

namespace Auth.Application.ExternalServices
{
    /// <summary>
    /// Контракт репозиторий для пользователей.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получения пользователя по наименованию.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Модель пользователя.</returns>
        public Task<User?> GetUserByName(string userName);

        /// <summary>
        /// Получение пользователя по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор пользователя.</param>
        /// <returns>Модель пользователя.</returns>
        public Task<User?> GetUserById(long id);

        /// <summary>
        /// Метод создания пользователя.
        /// </summary>
        /// <param name="user">Модель пользователя</param>
        /// <returns>Модель пользователя</returns>
        public Task<User> CreateUser(User user);

        /// <summary>
        /// Проверка пароля.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <param name="password">Пароль.</param>
        /// <returns>Флаг проверки. </returns>
        public Task<bool> CheckPassword(string userName, string password);

        /// <summary>
        /// Проверка есть ли пользователь по наименованию.
        /// </summary>
        /// <param name="userName">Имя пользователя.</param>
        /// <returns>Флаг проверки.</returns>
        public Task<bool> IsExistUserByName(string userName);
    }
}
