using Elibri.EF.DTOS;
using Elibri.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Elibri.Core.Features.CartServices
{
    public class CartService : ICartService
    {
        private readonly Context _context;

        public CartService(Context context)
        {
            _context = context;
        }

        // Получает все корзины асинхронно.
        public async Task<List<CartDTO>> GetAllAsync()
        {
            var carts = await _context.Carts
                .Select(c => new CartDTO
                {
                    CartId = c.CartId,
                    UserId = c.UserId,
                    CartItems = c.CartItems.Select(ci => new CartItemDTO
                    {
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity
                    }).ToList()
                }).ToListAsync();

            return carts;
        }

        // Получает корзину по идентификатору асинхронно.
        public async Task<CartDTO> GetByIdAsync(int id)
        {
            var cart = await _context.Carts
                .Where(c => c.CartId == id)
                .Select(c => new CartDTO
                {
                    CartId = c.CartId,
                    UserId = c.UserId,
                    CartItems = c.CartItems.Select(ci => new CartItemDTO
                    {
                        ProductId = ci.ProductId,
                        Quantity = ci.Quantity
                    }).ToList()
                }).FirstOrDefaultAsync();

            return cart;
        }

        // Создает новую корзину асинхронно.
        public async Task<CartDTO> CreateAsync(CartDTO cartDTO)
        {
            var cart = new Cart
            {
                UserId = cartDTO.UserId
            };

            _context.Carts.Add(cart);
            await _context.SaveChangesAsync();

            cartDTO.CartId = cart.CartId;

            return cartDTO;
        }

        // Обновляет существующую корзину асинхронно.
        public async Task UpdateAsync(CartDTO cartDTO)
        {
            var cart = await _context.Carts.FindAsync(cartDTO.CartId);
            if (cart != null)
            {
                cart.UserId = cartDTO.UserId;
                cart.CartItems = cartDTO.CartItems.Select(ci => new CartItem
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity
                }).ToList();

                _context.Carts.Update(cart);
                await _context.SaveChangesAsync();
            }
        }

        // Удаляет корзину по идентификатору асинхронно.
        public async Task DeleteAsync(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart != null)
            {
                _context.Carts.Remove(cart);
                await _context.SaveChangesAsync();
            }
        }

        // Добавляет продукт в корзину асинхронно.
        public async Task<ServiceResult> AddProductToCartAsync(int cartId, int productId, int quantity)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart == null)
            {
                return new ServiceResult { IsSuccess = false, ErrorMessage = "Корзина не найдена." };
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
            }
            else
            {
                cart.CartItems.Add(new CartItem
                {
                    ProductId = productId,
                    Quantity = quantity,
                    CartId = cartId
                });
            }

            await _context.SaveChangesAsync();
            return new ServiceResult { IsSuccess = true };
        }

        // Обновляет количество продукта в корзине асинхронно.
        public async Task<ServiceResult> UpdateProductQuantityInCartAsync(int cartId, int productId, int quantity)
        {
            var cart = await _context.Carts.FindAsync(cartId);
            if (cart == null)
            {
                return new ServiceResult { IsSuccess = false, ErrorMessage = "Корзина не найдена." };
            }

            var cartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);
            if (cartItem == null)
            {
                return new ServiceResult { IsSuccess = false, ErrorMessage = "Товар в корзине не найден." };
            }

            cartItem.Quantity = quantity;
            await _context.SaveChangesAsync();
            return new ServiceResult { IsSuccess = true };
        }
    }

    public class ServiceResult
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
