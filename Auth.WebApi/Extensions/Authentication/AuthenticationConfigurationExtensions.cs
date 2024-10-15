using Auth.Common.Model.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Auth.Extensions.Authentication
{
    /// <summary>
    /// Класс для настройки конфигурации аутентификации.
    /// </summary>
    public static class AuthenticationConfigurationExtensions
    {
        /// <summary>
        /// Настройка аутентификации JwtBearer.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        /// <param name="configuration">Конфигурация приложения.</param>
        public static IServiceCollection AddAuthenticationJWTBearer(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSectionConfiguration = configuration.GetSection("JWTConfiguration");

            services.Configure<JWTConfigurationOption>(jwtSectionConfiguration);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = jwtSectionConfiguration["Issuer"],
                    ValidAudience = jwtSectionConfiguration["Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSectionConfiguration["SecretKey"]))
                };
            });

            return services;
        }
    }
}
