using API.Web;
using Elibri.DTOs.DTOS;
using Elibri.Services.CartServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;


namespace API.Controllers
{
    [ApiController]

    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
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
        [ProducesResponseType(typeof(List<CartDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<CartDTO>>> GetAllCarts()
        {
            var carts = await _cartService.GetAllAsync();
            if (carts == null)
            {
                return Ok(new List<CartDTO>());
            }
            return Ok(carts);
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
        [ProducesResponseType(typeof(CartDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CartDTO>> GetCartById(int id)
        {
            var cart = await _cartService.GetByIdAsync(id);
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
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
        [ProducesResponseType(typeof(CartDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<CartDTO>> CreateCart(CartDTO cartDTO)
        {
            var createdCart = await _cartService.CreateAsync(cartDTO);
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
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Update(int id, CartDTO cartDTO)
        {
            var existingDto = await _cartService.GetByIdAsync(id);
            if (existingDto == null)
            {
                return NotFound();
            }
            await _cartService.UpdateAsync(cartDTO);
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
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteCart(int id)
        {
            await _cartService.DeleteAsync(id);
            return Ok("Успешно удалено.");
        }
    }
}
