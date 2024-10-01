using Auth.Application.ExternalServices;
using Auth.Application.Services;
using Auth.Infrastructure.Implementation;

namespace Auth.Extensions.Services
{
    public static class RegisterApplicationServiceExtension
    {
        public static IServiceCollection RegisterApplicationService(this IServiceCollection services)
        {
            services.AddScoped<AuthorizationService>();
            services.AddScoped<TokenService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ISessionRepository, SessionRepository>();
            return services;
        }
    }
}
