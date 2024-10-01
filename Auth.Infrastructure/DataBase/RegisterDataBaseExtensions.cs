using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Auth.Infrastructure.DataBase
{
    public static class RegisterDataBaseExtensions
    {
        public static IServiceCollection RegisterDataBase(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("ConnectionStrings")["DefaultConnection"];
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            services.AddDbContext<ApplicationContext>(opt =>
            {
                opt.UseNpgsql(connectionString);
            });

            return services;
        }

    }
}
