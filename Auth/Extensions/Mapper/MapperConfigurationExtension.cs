using Mapster;
using MapsterMapper;
using System.Reflection;

namespace Auth.Extensions.Mapper
{
    public static class MapperConfigurationExtension
    {
        /// <summary>
        /// Конфигурации маппера.
        /// </summary>
        /// <param name="services">Коллекция сервиса.</param>
        public static IServiceCollection ConfigurationMapper(this IServiceCollection services)
        {
            var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
            typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
            var mapperConfig = new MapsterMapper.Mapper(typeAdapterConfig);

            services.AddSingleton<IMapper>(mapperConfig);
            return services;
        }
    }
}
