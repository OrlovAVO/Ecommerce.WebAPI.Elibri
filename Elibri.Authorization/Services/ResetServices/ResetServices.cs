using Elibri.Authorization.Services.EmailServices;
using Elibri.Authorization.Services.ResetServices;
using Elibri.EF.Models;
using Elibri.Core.Repository.UserRepo;
using Microsoft.AspNetCore.Identity;
using Serilog;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Authorization.Services.ResetServices
{
    public class ResetService : IResetService
    {
        private readonly IUserRepository _userRepository;
        private readonly UserManager<User> _userManager;
        private readonly IEmailService _emailService;

        public ResetService(IUserRepository userRepository, UserManager<User> userManager, IEmailService emailService)
        {
            _userRepository = userRepository;
            _userManager = userManager;
            _emailService = emailService;
        }

        private string GenerateRandomPassword()
        {
            const string validChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder password = new StringBuilder();
            Random rnd = new Random();

            while (password.Length < 12)
            {
                password.Append(validChars[rnd.Next(validChars.Length)]);
            }

            return password.ToString();
        }

        public async Task<(bool Success, string ErrorMessage)> ResetPassword(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                Log.Information("Пользователь с адресом {Email} не найден", email);
                return (false, "Пользователь не найден");
            }

            // Генерация нового случайного пароля
            var newPassword = GenerateRandomPassword();

            // Установка нового пароля для пользователя
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (result.Succeeded)
            {
                Log.Information("Пользователю с адресом {Email} успешно установлен новый пароль", email);

                // Отправляем уведомление о сбросе пароля с новым паролем
                await _emailService.SendNewPasswordEmailAsync(email, newPassword);

                return (true, null);
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                Log.Error("Сброс пароля для пользователя с адресом {Email} не выполнен: {Errors}", email, errors);
                return (false, errors);
            }
        }

    }
}