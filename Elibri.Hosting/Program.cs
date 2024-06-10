using Elibri.Core.Registration;
using Elibri.EF.Registration;
using Elibri.Authorization.Registration;
using Elibri.Api.Registration;
using Microsoft.OpenApi.Models;

namespace Elibri.Hosting
{
    /// <summary>
    /// Главный класс приложения.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Точка входа в приложение.
        /// </summary>
        /// <param name="args">Аргументы командной строки.</param>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Загрузка конфигурации
            IConfiguration configuration = builder.Configuration;

            // Добавление сервисов в контейнер
            builder.Services.AddEFServices(configuration);
            builder.Services.AddCoreServices(configuration);
            builder.Services.AddAuthServices(configuration);
            builder.Services.AddApiServices(configuration);

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
            });

            // Добавление политики CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://25.32.59.44:3000", "http://localhost:3000")
                               .AllowAnyHeader()
                               .AllowAnyMethod();
                    });
            });

            var app = builder.Build();

            // Настройка конвейера HTTP-запросов
            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Включение Swagger для среды разработки или для профиля Swagger
                if (app.Environment.EnvironmentName == "Development" || app.Environment.EnvironmentName == "swagger")
                {
                    app.UseSwagger();
                    app.UseSwaggerUI(c =>
                    {
                        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Elibri API V1");
                    });
                }
            }

            app.UseCors("AllowSpecificOrigin");

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
