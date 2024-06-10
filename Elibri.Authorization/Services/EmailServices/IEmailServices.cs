using System.Threading.Tasks;

namespace Elibri.Authorization.Services.EmailServices
{
    // Интерфейс для сервиса отправки электронной почты.
    public interface IEmailService
    {
        // Метод для асинхронной отправки письма с новым паролем.
        // Параметры:
        // - email: адрес электронной почты получателя.
        // - newPassword: новый пароль, который будет отправлен в письме.
        Task SendNewPasswordEmailAsync(string email, string newPassword);
    }
}
