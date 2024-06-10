using Elibri.EF.DTOS;

namespace Elibri.Core.Features.CartServices
{
    // Интерфейс для сервиса работы с корзинами.
    public interface ICartService
    {
        // Получает все корзины асинхронно.
        Task<List<CartDTO>> GetAllAsync();

        // Получает корзину по идентификатору асинхронно.
        Task<CartDTO> GetByIdAsync(int id);

        // Создает новую корзину асинхронно.
        Task<CartDTO> CreateAsync(CartDTO cartDTO);

        // Обновляет существующую корзину асинхронно.
        Task UpdateAsync(CartDTO cartDTO);

        // Удаляет корзину по идентификатору асинхронно.
        Task DeleteAsync(int id);

        // Добавляет продукт в корзину асинхронно.
        Task<ServiceResult> AddProductToCartAsync(int cartId, int productId, int quantity);

        // Обновляет количество продукта в корзине асинхронно.
        Task<ServiceResult> UpdateProductQuantityInCartAsync(int cartId, int productId, int quantity);
    }
}
