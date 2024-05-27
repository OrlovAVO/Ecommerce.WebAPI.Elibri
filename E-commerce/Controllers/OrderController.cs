using API.Web;
using Elibri.DTOs.DTOS;
using Elibri.Services.OrderServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
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
                return NotFound();
            }
            return Ok(order);
        }

        /// <summary>
        /// Создание заказа
        /// </summary>
        /// <remarks>
        /// Для создания заказа нужно авторизироваться, ввести UserId и OrderId
        /// </remarks>
        [HttpPost]
        [Route(Routes.CreateOrderRoute)]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<OrderDTO>> CreateOrder(OrderDTO orderDTO)
        {
            var createdOrder = await _orderService.CreateAsync(orderDTO);
            return Ok(createdOrder);
        }

        /// <summary>
        /// Обновление заказа
        /// </summary>
        /// <remarks>
        /// Для обновления заказа нужно авторизироваться, ввести UserId и OrderId
        /// </remarks>
        [HttpPut]
        [Route(Routes.UpdateOrderRoute)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Update(int id, OrderDTO orderDTO)
        {
            var existingDto = await _orderService.GetByIdAsync(id);
            if (existingDto == null)
            {
                return NotFound();
            }
            await _orderService.UpdateAsync(orderDTO);
            return Ok("Товар успешно обновлён.");
        }

        /// <summary>
        /// Удаление заказа по UserId и OrderId
        /// </summary>
        /// <remarks>
        /// Для удаления заказа нужно авторизироваться и ввести UserId и OrderId
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
