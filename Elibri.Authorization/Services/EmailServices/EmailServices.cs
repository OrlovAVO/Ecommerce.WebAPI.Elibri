using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Authorization.Services.EmailServices
{
    public class EmailService : IEmailService
    {

        private readonly string _frontendUrl;
        private readonly IConfiguration _configuration;

        public EmailService(string frontendUrl, IConfiguration configuration)
        {
            _frontendUrl = frontendUrl;
            _configuration = configuration;
        }

        public async Task SendNewPasswordEmailAsync(string email, string newPassword)
        {
            var smtpHost = _configuration["EmailSettings:SmtpHost"];
            var smtpPort = int.Parse(_configuration["EmailSettings:SmtpPort"]);
            var smtpUsername = _configuration["EmailSettings:SmtpUsername"];
            var smtpPassword = _configuration["EmailSettings:SmtpPassword"];

            using (var client = new SmtpClient(smtpHost, smtpPort))
            {
                client.UseDefaultCredentials = false;
                client.Credentials = new NetworkCredential(smtpUsername, smtpPassword);
                client.EnableSsl = true;

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(smtpUsername, "Elibri – Интернет-магазин цифровой и бытовой техники"),
                    Subject = "Сброс пароля",
                    Body = $"Вы запросили смену пароля. " +
                    $"Ваш новый пароль: {newPassword}",

                    IsBodyHtml = false,
                    BodyEncoding = Encoding.UTF8
                };

                mailMessage.To.Add(email);

                try
                {
                    await client.SendMailAsync(mailMessage);
                }
                catch (Exception ex)
                {
                    throw new Exception("Не удалось отправить электронное письмо: " + ex.Message);
                }
            }
        }
    }
}
