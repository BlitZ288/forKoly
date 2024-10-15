using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Auth.Extensions
{
    /// <summary>
    /// Класс расширения для настройки и конфигурации swagger
    /// </summary>
    public static class SwaggerDocumentExtension
    {
        /// <summary>
        /// Настройка генерации swagger документации.
        /// </summary>
        /// <param name="services">Коллекция сервисов.</param>
        public static IServiceCollection SwaggerGenerateDocument(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var filePath = Path.Combine(AppContext.BaseDirectory, fileName);

                options.IncludeXmlComments(filePath);

                options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "Введите строку авторизации на предъявителя следующим образом: `Bearer JWTТокен`",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }

                }); ;
            });

            return services;
        }

        /// <summary>
        /// Регистрации и настройка промежуточного ПО для Swagger UI.
        /// </summary>
        /// <param name="builder">Конвейер настройки приложения</param>
        public static IApplicationBuilder UseSwaggerWithUI(this IApplicationBuilder builder)
        {
            builder.UseSwagger();

            builder.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                options.RoutePrefix = string.Empty;
            });

            return builder;
        }
    }
}
