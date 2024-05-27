using API.Web;
using Elibri.DTOs.DTOS;
using Elibri.Services.CartServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;



namespace API.Controllers
{
    [ApiController]

    public class CartController : ControllerBase
    {
        private readonly ICartService _CartService;

        public CartController(ICartService CartService)
        {
            _CartService = CartService;
        }

        /// <summary>
        /// Получение всех корзин
        /// </summary>
        /// <remarks>
        /// Для получения корзин нужно авторизироваться
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetCartsRoute)]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<List<CartDTO>>> GetAllCarts()
        {
            var Carts = await _CartService.GetAllAsync();
            if (Carts == null)
            {
                return Ok(new List<CartDTO>());
            }
            return Ok(Carts);
        }

        /// <summary>
        /// Получение корзины по UserId и CartId
        /// </summary>
        /// <remarks>
        /// Для получения корзины нужно авторизироваться, ввести UserId и CartId
        /// </remarks>
        [HttpGet]
        [Route(Routes.GetCartByIdRoute)]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<CartDTO>> GetCartById(int id)
        {
            var Cart = await _CartService.GetByIdAsync(id);
            if (Cart == null)
            {
                return NotFound();
            }
            return Ok(Cart);
        }

        /// <summary>
        /// Создание корзины
        /// </summary>
        /// <remarks>
        /// Для создания корзины нужно авторизироваться, ввести UserId и CartId
        /// </remarks>
        [HttpPost]
        [Route(Routes.CreateCartRoute)]
        [Authorize(Roles = "User")]
        public async Task<ActionResult<CartDTO>> CreateCart(CartDTO CartDTO)
        {
            var createdCart = await _CartService.CreateAsync(CartDTO);

            return Ok(createdCart);
        }

        /// <summary>
        /// Обновление корзины
        /// </summary>
        /// <remarks>
        /// Для обновления корзины нужно авторизироваться, ввести UserId и CartId
        /// </remarks>
        [HttpPut]
        [Route(Routes.UpdateCartByIdRoute)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> Update(int id, CartDTO CartDTO)
        {
            var existingDto = await _CartService.GetByIdAsync(id);
            if (existingDto == null)
            {
                return NotFound();
            }
            await _CartService.UpdateAsync(CartDTO);
            return Ok("Успешно обновлено.");
        }

        /// <summary>
        /// Удаление корзины по UserId и CartId
        /// </summary>
        /// <remarks>
        /// Для удаления корзины нужно авторизироваться и ввести UserId и CartId
        /// </remarks>
        [HttpDelete]
        [Route(Routes.DeleteCartByIdRoute)]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            await _CartService.DeleteAsync(id);
            return Ok("Успешно удалено.");
        }
    }


}

