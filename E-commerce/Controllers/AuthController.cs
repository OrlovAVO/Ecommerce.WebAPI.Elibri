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
using API.Web;
using System.Net;


namespace API.Controllers
{

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
        /// <summary>
        /// Регистрация нового Admin
        /// </summary>
        /// <remarks>
        /// Для регистрации нужен username, email и пароль
        /// </remarks>
        [HttpPost]
        [Route(Routes.AdminRegistrationRoute)]
        [ProducesResponseType(typeof(LoginDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin(RegisterDto model)
        {
            return await _authService.RegisterAdmin(model);
        }

        /// <summary>
        /// Регистрация нового User
        /// </summary>
        /// <remarks>
        /// Для регистрации нужен username, email и пароль
        /// </remarks>
        [HttpPost]
        [Route(Routes.RegistrationRoute)]
        [ProducesResponseType(typeof(LoginDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterUser(RegisterDto model)
        {
            return await _authService.RegisterUser(model);
        }

        /// <summary>
        /// Вход и получение токена через UserName
        /// </summary>
        /// <remarks>
        /// Для аутентификации необходимо ввести Username и пароль
        /// </remarks>
        /// <returns>Токен</returns>
        [HttpPost]
        [Route(Routes.LoginRoute)]
        [ProducesResponseType(typeof(LoginDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login(LoginDto model)
        {
            return await _authService.Login(model);
        }

        /// <summary>
        /// Сброс пароля по email
        /// </summary>
        /// <remarks>
        /// Для сброса пароля нужно указать email
        /// </remarks>
        [HttpPost]
        [Route(Routes.ResetPassword)]
        [ProducesResponseType(typeof(LoginDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
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
        /// <summary>
        /// Смена пароля через авторизированный аккаунт
        /// </summary>
        /// <remarks>
        /// Для сброса пароля нужно указать старый пароль
        /// </remarks>
        [HttpPost]
        [Route(Routes.ChangePassword)]
        [ProducesResponseType(typeof(LoginDto), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [Authorize(Roles = "User")] 
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
