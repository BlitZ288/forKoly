using Asp.Versioning;

namespace Auth.Extensions
{
    /// <summary>
    /// Класс расширения для настройки версионирования API.
    /// </summary>
    public static class VersionApiExtension
    {
        /// <summary>
        /// Метод регистрации сервиса версионирования
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        public static IServiceCollection RegisterVersionApi(this IServiceCollection services)
        {
            services.AddApiVersioning(
                options =>
                {
                    options.ReportApiVersions = true;

                    options.Policies.Sunset(0.9)
                                        .Effective(DateTimeOffset.Now.AddDays(60))
                                        .Link("policy.html")
                                            .Title("Versioning Policy")
                                            .Type("text/html");
                }).AddApiExplorer(opt =>
                {
                    //"'v'major[.minor][-status]"
                    opt.GroupNameFormat = "'v'VVV";
                    opt.SubstituteApiVersionInUrl = true;
                });

            return services;
        }
    }
}
