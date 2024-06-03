using Elibri.Api.Web;
using Elibri.EF.DTOS;
using Elibri.Core.Features.OrderServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Elibri.Api.Controllers
{
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Получение всех заказов
        /// </summary>
        /// <remarks>
        /// Для получения заказов нужно авторизироваться
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetAllOrdersRoute)]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<List<OrderDTO>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        /// <summary>
        /// Получение корзины по UserId и OrderId
        /// </summary>
        /// <remarks>
        /// Для получения заказов нужно авторизироваться, ввести UserId и OrderId
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetOrderByIdRoute)]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            var order = await _orderService.GetByIdAsync(id);
            if (order == null)
            {
                return NotFound("Пользователь не найден");
            }
            return Ok(order);
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <remarks>
        /// Для создания заказа нужно авторизироваться
        /// </remarks>
        [HttpPost]
        [Route(Routes.CreateOrderRoute)]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<ServiceResult<OrderDTO>>> CreateOrder(CreateOrderDTO createOrderDto)
        {
            var result = await _orderService.CreateOrderAsync(createOrderDto);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(result);
        }

        /// <summary>
        /// Удаление заказа
        /// </summary>
        /// <remarks>
        /// Для удаления заказа нужно иметь права администратора
        /// </remarks>
        [HttpDelete]
        [Route(Routes.DeleteOrderRoute)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteAsync(id);
            return Ok("Товар успешно удалён.");
        }
    }
}
