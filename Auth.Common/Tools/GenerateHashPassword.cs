using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace Auth.Common.Tools
{
    public static class GenerateHashPassword
    {
        /// <summary>
        /// Функция генерации hash пароля.
        /// </summary>
        /// <param name="password">Пароль.</param>
        /// <returns>Кортеж состоящий из hash пароля и соли.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static (string passwordHash, string salt) GetGenerateHashPasswordWithSalt(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException("password");

            var salt = RandomNumberGenerator.GetBytes(128 / 8);
            var key = KeyDerivation.Pbkdf2(
             password: password,
             salt: salt,
             prf: KeyDerivationPrf.HMACSHA256,
             iterationCount: 100000,
             numBytesRequested: 256 / 8);
            var hash = Convert.ToBase64String(key);

            return (hash, Convert.ToBase64String(salt));
        }

        /// <summary>
        /// Получение hash пароля по известной соли.
        /// </summary>
        /// <param name="password">Пароль.</param>
        /// <param name="salt">Соль.</param>
        /// <returns>Сгенерированный hash.</returns>
        public static string GetPasswordHash(string password, string salt)
        {
            var key = KeyDerivation.Pbkdf2(
             password: password,
             salt: Convert.FromBase64String(salt),
             prf: KeyDerivationPrf.HMACSHA256,
             iterationCount: 100000,
             numBytesRequested: 256 / 8);

            var hash = Convert.ToBase64String(key);

            return hash;
        }
    }
}