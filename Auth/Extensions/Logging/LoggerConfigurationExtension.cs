using Serilog;
using Serilog.Events;

namespace Auth.Extensions
{
    /// <summary>
    /// Класс для настройки и встраивание в промежуточное ПО логгирования
    /// </summary>
    public static class LoggerConfigurationExtension
    {
        /// <summary>
        /// Настройка и встраивание в промежуточное ПО серилог.
        /// </summary>
        /// <param name="builder">Конвейер настройки приложения</param>
        public static IApplicationBuilder UseSerilog(this IApplicationBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
              .WriteTo.Console()
              .WriteTo.File("log.txt",
                      rollingInterval: RollingInterval.Day,
                      outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
              .MinimumLevel.Information()
              .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
              .MinimumLevel.Override("Microsoft.Extensions.Hosting", LogEventLevel.Information)
              .MinimumLevel.Override("Microsoft.Hosting", LogEventLevel.Information)
              .CreateLogger();

            builder.UseSerilogRequestLogging();
            return builder;
        }
    }
}
