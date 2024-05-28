using API.Web;
using Elibri.DTOs.DTOS;
using Elibri.Services.OrderDetailsServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]

    public class OrderDetailsController : ControllerBase
    {
        private readonly IOrderDetailService _OrderDetailsService;

        public OrderDetailsController(IOrderDetailService OrderDetailsService)
        {
            _OrderDetailsService = OrderDetailsService;
        }

        /// <summary>
        /// Получение всех деталей всех заказов
        /// </summary>
        /// <remarks>
        /// Для получения всех деталей всех заказов нужно авторизироваться
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetAllOrdersDetailsRoute)]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<List<OrderDetailDTO>>> GetAllOrderDetailss()
        {
            var OrderDetailss = await _OrderDetailsService.GetAllAsync();
            if (OrderDetailss == null)
            {
                return Ok(new List<OrderDetailDTO>());
            }
            return Ok(OrderDetailss);
        }

        /// <summary>
        /// Получение корзины по UserId и OrderDetailId
        /// </summary>
        /// <remarks>
        /// Для получения деталей заказа нужно авторизироваться, ввести UserId и OrderDetailId
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetOrderDetailByIdRoute)]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<OrderDetailDTO>> GetOrderDetailsById(int id)
        {
            var OrderDetails = await _OrderDetailsService.GetByIdAsync(id);
            if (OrderDetails == null)
            {
                return NotFound();
            }
            return Ok(OrderDetails);
        }

        /// <summary>
        /// Создание деталей заказа
        /// </summary>
        /// <remarks>
        /// Для создания деталей заказа нужно авторизироваться, ввести UserId и OrderDetailId
        /// </remarks>
        [HttpPost]
        [Route(Routes.CreateOrderDetailRoute)]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<OrderDetailDTO>> CreateOrderDetails(OrderDetailDTO OrderDetailsDTO)
        {
            var createdOrderDetails = await _OrderDetailsService.CreateAsync(OrderDetailsDTO);

            return Ok(createdOrderDetails);
        }

        /// <summary>
        /// Обновление деталей заказа
        /// </summary>
        /// <remarks>
        /// Для обновления деталей заказа нужно авторизироваться, ввести UserId и OrderDetailId
        /// </remarks>
        [HttpPut]
        [Route(Routes.UpdateOrderDetailRoute)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Update(int id, OrderDetailDTO OrderDetailsDTO)
        {
            var existingDto = await _OrderDetailsService.GetByIdAsync(id);
            if (existingDto == null)
            {
                return NotFound();
            }
            await _OrderDetailsService.UpdateAsync(OrderDetailsDTO);
            return Ok("Updated Successfuly");
        }

        /// <summary>
        /// Удаление деталей заказа по UserId и OrderDetailId
        /// </summary>
        /// <remarks>
        /// Для удаления деталей заказа нужно авторизироваться и ввести UserId и OrderDetailId
        /// </remarks>
        [HttpDelete]
        [Route(Routes.DeleteOrderDetailRoute)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteOrderDetails(int id)
        {
            await _OrderDetailsService.DeleteAsync(id);
            return Ok("Deleted Successfuly");
        }
    }


}

