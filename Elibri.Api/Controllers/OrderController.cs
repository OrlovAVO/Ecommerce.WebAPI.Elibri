using Elibri.Api.Web;
using Elibri.Core.Features.OrderServices;
using Elibri.EF.DTOS;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        /// Получение заказов всех пользователей
        /// </summary>
        /// <remarks>
        /// Для получения заказов всех пользователей нужно иметь права администратора
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetAllOrdersRoute)]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<OrderDTO>>> GetAllOrders()
        {
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            await _orderService.DeleteAsync(id);
            return Ok("Заказ успешно удалён.");
        }
    }
}
