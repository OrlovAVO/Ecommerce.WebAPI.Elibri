using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Elibri.DTOs.DTOS;
using Elibri.Services.AuthServices;
using Microsoft.AspNetCore.Identity.Data;
using Elibri.Services.EmailServices;
using Elibri.Services.TokenServices;
using Elibri.Services.UserServices;
using Elibri.Services.ResetServices;
using Serilog;
using Elibri.Repositories.UserRepo;
using Elibri.Models;


namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly IResetService _resetService;
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<User> _userManager;

        public AuthController(IUserService userService, IAuthService authService, IEmailService emailService, IResetService resetService, ILogger<AuthController> logger, UserManager<User> userManager)
        {
            _authService = authService;
            _userService = userService;
            _emailService = emailService;
            _resetService = resetService;
            _logger = logger;
            _userManager = userManager;

        }

        [HttpPost("RegisterAdmin")]
/*        [Authorize(Roles = "Admin")]*/
        public async Task<IActionResult> RegisterAdmin(RegisterDto model)
        {
            return await _authService.RegisterAdmin(model);
        }

        [HttpPost("RegisterUser")]
        public async Task<IActionResult> RegisterUser(RegisterDto model)
        {
            return await _authService.RegisterUser(model);
        }
        /*            [HttpPost]
                    [Route(Routes.EmailLoginRoute)]
                    [ProducesResponseType(typeof(AuthResponse), (int)HttpStatusCode.OK)]
                    [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]*/
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto model)
        {
            return await _authService.Login(model);
        }


        [HttpPost("Reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                var (success, errorMessage) = await _resetService.ResetPassword(model.Email);
                if (success)
                {
                    return Ok("Сброс пароля успешен. Новый пароль отправлен на вашу электронную почту.");
                }
                else
                {
                    Log.Error("Не удалось сбросить пароль для {Email}: {ErrorMessage}", model.Email, errorMessage);
                    return BadRequest($"Сброс пароля не выполнен: {errorMessage}");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("Change-password")]
        [Authorize] // Для ограничения доступа только для аутентифицированных пользователей
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound("Пользователь не найден");
                }

                var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    return Ok("Пароль успешно изменен");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }

            return BadRequest("Некорректная модель");
        }






    }

}
