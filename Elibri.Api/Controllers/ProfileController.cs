using Elibri.Api.Web;
using Elibri.Authorization.DTOS;
using Elibri.Authorization.Services.AuthServices;
using Elibri.Authorization.Services.EmailServices;
using Elibri.Authorization.Services.ResetServices;
using Elibri.Core.Features.OrderServices;
using Elibri.Core.Features.UserServices;
using Elibri.EF.DTOS;
using Elibri.EF.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;


namespace Elibri.API.Controllers
{

    [ApiController]

    public class ProfileController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;
        private readonly IUserService _userService;
        private readonly IResetService _resetService;
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IOrderService _orderService;

        public ProfileController(IUserService userService, IAuthService authService, IEmailService emailService, IResetService resetService, ILogger<AuthController> logger, UserManager<User> userManager, IOrderService orderService)
        {
            _authService = authService;
            _userService = userService;
            _emailService = emailService;
            _resetService = resetService;
            _logger = logger;
            _userManager = userManager;
            _orderService = orderService;
        }


        /// <summary>
        /// Смена пароля через авторизированный аккаунт
        /// </summary>
        /// <remarks>
        /// Для сброса пароля нужно указать старый пароль
        /// </remarks>
        [HttpPost]
        [Route(Routes.ProfileChangePassword)]
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

        /// <summary>
        /// Получение заказов авторизованного пользователя 
        /// </summary>
        /// <remarks>
        /// Для получения заказов нужно авторизироваться и передать в headers токен для дальнейшей идентификации
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetOrderByIdRoute)]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<List<OrderDTO>>> GetOrderById()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null)
            {
                return BadRequest("Пользователь не найден.");
            }

            var orders = await _orderService.GetOrdersByUserIdAsync(userId);
            if (orders == null || !orders.Any())
            {
                return NotFound("У пользователя нет заказов.");
            }

            return Ok(orders);
        }

    }

}
