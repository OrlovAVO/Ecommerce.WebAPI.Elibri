/*using Elibri.EF.DTOS;
using Elibri.Core.Features.CartServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Collections.Generic;
using System.Threading.Tasks;
using Elibri.Api.Web;

namespace Elibri.Api.Controllers
{
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

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

        [HttpPost]
        [Route(Routes.AddProductToCartRoute)]
        [Authorize(Roles = "User")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AddProductToCart(int cartId, int productId, int quantity)
        {
            var existingCart = await _cartService.GetByIdAsync(cartId);

            if (existingCart == null)
            {
                var newCartDto = new CartDTO
                {
                    CartId = cartId,
                    UserId = User.Identity.Name,
                };
                existingCart = await _cartService.CreateAsync(newCartDto);
            }

            var result = await _cartService.AddProductToCartAsync(cartId, productId, quantity);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok("Товар успешно добавлен в корзину.");
        }

        [HttpPut]
        [Route(Routes.UpdateProductQuantityInCartRoute)]
        [Authorize(Roles = "User")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateProductQuantityInCart(int cartId, int productId, int quantity)
        {
            var result = await _cartService.UpdateProductQuantityInCartAsync(cartId, productId, quantity);
            if (!result.IsSuccess)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok("Количество товара в корзине успешно изменено.");
        }
    }
}
*/