using Auth.Extensions;
using Auth.Extensions.Authentication;
using Auth.Extensions.Mapper;
using Auth.Extensions.Services;
using Auth.Filter;
using Auth.Infrastructure.DataBase;
using Serilog;

namespace Auth
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.RegisterVersionApi();

            builder.Services.SwaggerGenerateDocument();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSerilog();

            builder.Services.AddAuthenticationJWTBearer(builder.Configuration);
            builder.Services.AddAuthorization();

            builder.Services.RegisterApplicationService();
            builder.Services.RegisterDataBase(builder.Configuration);

            builder.Services.AddScoped<AuthorizationFilter>();

            builder.Services.ConfigurationMapper();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.UseSerilogRequestLogging();

            app.MapDefaultControllerRoute();

            app.UseSerilog();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwaggerWithUI();

            app.Run();
        }
    }
}
