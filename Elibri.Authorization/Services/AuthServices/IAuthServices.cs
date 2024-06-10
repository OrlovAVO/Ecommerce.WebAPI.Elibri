using Elibri.Authorization.DTOS;
using Microsoft.AspNetCore.Mvc;

namespace Elibri.Authorization.Services.AuthServices
{
    // Интерфейс для сервисов аутентификации.
    public interface IAuthService
    {
        // Регистрирует администратора.
        Task<IActionResult> RegisterAdmin(RegisterDto model);

        // Регистрирует обычного пользователя.
        Task<IActionResult> RegisterUser(RegisterDto model);

        // Выполняет аутентификацию пользователя.
        Task<IActionResult> Login(LoginDto model);
    }
}
