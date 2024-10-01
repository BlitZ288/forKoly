namespace Auth.Common.Model.Configuration
{
    /// <summary>
    /// Класс настройки конфигурации JWT токена
    /// </summary>
    public class JWTConfigurationOption
    {
        /// <summary>
        /// Секретный ключ.
        /// </summary>
        public required string SecretKey { get; set; }

        /// <summary>
        /// Идентифицирует принципала, выдавшего JWT.
        /// </summary>
        public required string Issuer { get; set; }

        /// <summary>
        /// Определяет получателей, для которых предназначен JWT.
        /// </summary>
        public required string Audience { get; set; }

        /// <summary>
        /// Определяет время, в течение которого сгенерированный JWT будет оставаться действительным.
        /// </summary>
        public required int DurationInMinutes { get; set; }
    }
}
