using Elibri.EF.Models;
using Elibri.Authorization.Services.AuthServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Elibri.Authorization.Services.EmailServices;
using Elibri.Authorization.Services.ResetServices;
using Elibri.Authorization.Services.TokenServices;

namespace Elibri.Authorization.Registration
{
    // Класс для расширения IServiceCollection, предоставляющий методы регистрации сервисов аутентификации и авторизации.
    public static class AuthorizationRegistrationExtensions
    {
        // Метод расширения для регистрации сервисов аутентификации и авторизации.
        public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Настройка Identity для пользователей (User) и ролей (IdentityRole).
            services.AddIdentity<User, IdentityRole>(op =>
            {
                // Отключение требования подтверждения аккаунта при входе.
                op.SignIn.RequireConfirmedAccount = false;
                // Отключение требования к наличию специальных символов в пароле.
                op.Password.RequireNonAlphanumeric = false;
                // Отключение требования к наличию цифр в пароле.
                op.Password.RequireDigit = false;
                // Отключение требования к наличию заглавных букв в пароле.
                op.Password.RequireUppercase = false;
            })
            // Указание использования Entity Framework для хранения информации о пользователях и ролях.
            .AddEntityFrameworkStores<Context>()
            // Добавление токенов по умолчанию для операций, таких как сброс пароля.
            .AddDefaultTokenProviders();

            // Настройка аутентификации с использованием JWT.
            services.AddAuthentication(op =>
            {
                // Установка схемы аутентификации по умолчанию как JwtBearer.
                op.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                op.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(op =>
            {
                // Настройка параметров валидации токена.
                op.TokenValidationParameters = new TokenValidationParameters
                {
                    // Ключ для подписи токена.
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwt:Key"])),
                    // Валидация ключа подписи.
                    ValidateIssuerSigningKey = true,
                    // Указание допустимой аудитории (получателей) токена.
                    ValidAudience = configuration["jwt:audience"],
                    // Валидация аудитории токена.
                    ValidateAudience = true,
                    // Указание допустимого издателя токена.
                    ValidIssuer = configuration["jwt:issuer"],
                    // Валидация издателя токена.
                    ValidateIssuer = true,
                };

                // Обработка события получения токена из HTTP-запроса.
                op.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // Получение токена из куки 'jwt-cookies'.
                        context.Token = context.Request.Cookies["jwt-cookies"];
                        return Task.CompletedTask;
                    }
                };
            });

            // Регистрация AuthService для управления аутентификацией.
            services.AddScoped<IAuthService, AuthService>();
            // Регистрация TokenService для управления токенами.
            services.AddScoped<ITokenService, TokenService>();
            // Регистрация ResetService для управления сбросом пароля.
            services.AddScoped<IResetService, ResetService>();

            // Регистрация EmailService с конфигурацией из настроек.
            services.AddScoped<IEmailService>(provider =>
            {
                // Получение конфигурации.
                var config = provider.GetRequiredService<IConfiguration>();
                // Получение URL фронтенда из конфигурации.
                var frontendUrl = config["FrontendUrl"];
                // Создание нового экземпляра EmailService с использованием URL фронтенда и конфигурации.
                return new EmailService(frontendUrl, config);
            });
        }
    }
}
