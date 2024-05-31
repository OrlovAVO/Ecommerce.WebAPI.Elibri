using Elibri.EF.DTOS;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elibri.Core.Features.CartServices
{
    public class CartService : ICartService
    {
        private readonly List<CartDTO> _carts = new List<CartDTO>();

        public async Task<List<CartDTO>> GetAllAsync()
        {
            return await Task.FromResult(_carts);
        }

        public async Task<CartDTO> GetByIdAsync(int id)
        {
            var cart = _carts.FirstOrDefault(c => c.CartId == id);
            return await Task.FromResult(cart);
        }

        public async Task<CartDTO> CreateAsync(CartDTO cartDTO)
        {
            _carts.Add(cartDTO);
            return await Task.FromResult(cartDTO);
        }

        public async Task UpdateAsync(CartDTO cartDTO)
        {
            var existingCart = _carts.FirstOrDefault(c => c.CartId == cartDTO.CartId);
            if (existingCart != null)
            {
                existingCart.UserId = cartDTO.UserId;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(int id)
        {
            var cart = _carts.FirstOrDefault(c => c.CartId == id);
            if (cart != null)
            {
                _carts.Remove(cart);
            }
            await Task.CompletedTask;
        }
    }
}
