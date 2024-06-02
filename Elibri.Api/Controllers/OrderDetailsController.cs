using Elibri.Api.Web;
using Elibri.Core.Features.OrderDetailsServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Elibri.EF.DTOS;

namespace Elibri.Api.Controllers
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
        public async Task<ActionResult<List<OrderDetailDTO>>> GetAllOrderDetails()
        {
            var orderDetails = await _OrderDetailsService.GetAllAsync();
            if (orderDetails == null || orderDetails.Count == 0)
            {
                return Ok(new List<OrderDetailDTO>());
            }
            return Ok(orderDetails);
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
        public async Task<ActionResult<List<OrderDetailDTO>>> GetOrderDetailsByUserId(string userId)
        {
            var orderDetails = await _OrderDetailsService.GetOrderDetailsByUserIdAsync(userId);
            if (orderDetails == null || orderDetails.Count == 0)
            {
                return NotFound();
            }
            return Ok(orderDetails);
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
        public async Task<IActionResult> Update(int id, OrderDetailDTO orderDetailsDTO)
        {
            var existingDto = await _OrderDetailsService.GetByIdAsync(id);
            if (existingDto == null)
            {
                return NotFound();
            }
            orderDetailsDTO.OrderDetailId = id;
            await _OrderDetailsService.UpdateAsync(orderDetailsDTO);
            return Ok("Updated Successfully");
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
            return Ok("Deleted Successfully");
        }
    }


}

