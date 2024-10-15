namespace Auth.Application.Models.Authorization
{
    /// <summary>
    /// Транспортная амортизационная модель.
    /// </summary>
    public class LoginResponseDto
    {
        /// <summary>
        /// Токен авторизации.
        /// </summary>
        public required string AccessToken { get; set; }

        /// <summary>
        /// Токен обновления.
        /// </summary>
        public required string RefreshToken { get; set; }
    }
}
