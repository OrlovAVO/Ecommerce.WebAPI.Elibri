using Microsoft.OpenApi.Models;
using System.Reflection;

namespace Elibri.Api.Registration
{
    // Класс содержит методы расширения для регистрации сервисов API.
    public static class ApiRegistrationExtensions
    {
        // Метод для регистрации сервисов API.
        public static void AddApiServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Добавляем поддержку контроллеров.
            services.AddControllers();

            // Добавляем поддержку Endpoints API Explorer для автоматического определения конечных точек API.
            services.AddEndpointsApiExplorer();

            // Настраиваем Swagger для генерации документации API.
            services.AddSwaggerGen(c =>
            {
                // Настройка для включения требований безопасности в Swagger.
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });

                // Включаем XML комментарии для генерации документации API.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            // Настраиваем политику CORS (Cross-Origin Resource Sharing).
            services.AddCors(options =>
            {
                // Добавляем стандартную политику CORS.
                options.AddDefaultPolicy(builder =>
                {
                    // Разрешаем запросы из конкретных источников, указанных в конфигурации.
                    builder.WithOrigins(configuration["FrontendUrl"], "http://25.49.57.113:3000")
                           .AllowAnyHeader() // Разрешаем любые заголовки.
                           .AllowAnyMethod(); // Разрешаем любые HTTP методы.
                });
            });
        }
    }
}
