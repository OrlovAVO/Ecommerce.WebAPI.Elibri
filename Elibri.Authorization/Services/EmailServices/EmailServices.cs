using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Authorization.Services.EmailServices
{
    // Реализация сервиса отправки электронной почты.
    public class EmailService : IEmailService
    {
        // URL фронтенда для использования в сообщениях электронной почты.
        private readonly string _frontendUrl;
        // Конфигурация приложения для получения настроек SMTP.
        private readonly IConfiguration _configuration;

        // Конструктор, инициализирующий URL фронтенда и конфигурацию.
        public EmailService(string frontendUrl, IConfiguration configuration)
        {
            _frontendUrl = frontendUrl;
            _configuration = configuration;
        }

        // Асинхронный метод для отправки письма с новым паролем.
        public async Task SendNewPasswordEmailAsync(string email, string newPassword)
        {
            // Получение настроек SMTP из конфигурации.
            var smtpHost = _configuration["EmailSettings:SmtpHost"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var smtpUsername = _configuration["EmailSettings:SmtpUsername"];
            var smtpPassword = _configuration["EmailSettings:SmtpPassword"];

            // Создание клиента SMTP для отправки электронной почты.
            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.UseDefaultCredentials = false; // Отключение использования учетных данных по умолчанию.
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword); // Установка учетных данных SMTP.
                client.EnableSsl = true; // Включение SSL для безопасной отправки.

                // Настройка сообщения электронной почты.
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpUsername, "Elibri – Интернет-магазин цифровой и бытовой техники"), // Установка отправителя.
                    Subject = "Сброс пароля", // Установка темы письма.
                    Body = $"Вы запросили смену пароля. Ваш новый пароль: {newPassword}", // Тело письма с новым паролем.
                    IsBodyHtml = false, // Указание, что тело письма не содержит HTML.
                    BodyEncoding = Encoding.UTF8 // Установка кодировки тела письма.
                };

                // Добавление получателя письма.
                mailMessage.To.Add(email);

                try
                {
                    // Асинхронная отправка письма.
                    await client.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    // Обработка ошибок при отправке письма.
                    throw new Exception("Не удалось отправить электронное письмо: " + ex.Message);
                }
            }
        }
    }
}
