using Elibri.EF.DTOS;

namespace Elibri.Core.Features.ProductServices
{
    // Интерфейс сервиса для управления продуктами.
    public interface IProductService
    {
        // Получает все продукты асинхронно с разбиением на страницы.
        Task<PagedResult<ProductDTO>> GetAllAsync(int pageNumber, int pageSize);

        // Получает продукт по идентификатору асинхронно.
        Task<ProductDTO> GetByIdAsync(int id);

        // Создает продукт асинхронно.
        Task<ProductDTO> CreateAsync(ProductDTO productDTO);

        // Получает все продукты по идентификатору категории асинхронно с разбиением на страницы.
        Task<PagedResult<ProductDTO>> GetProductsByCategoryIdAsync(int categoryId, int pageNumber = 1, int pageSize = 10);

        // Фильтрует продукты асинхронно по различным параметрам с разбиением на страницы.
        Task<PagedResult<ProductDTO>> FilterProductsAsync(
            int? categoryId,
            int? maxDeliveryDays,
            string sortOrder,
            string searchTerm,
            int pageNumber = 1,
            int pageSize = 10);

        // Получает продукты с из той же категогрии.
        Task<ProductWithRelatedDTO> GetProductWithRelatedAsync(int productId);

        // Получает продукт по имени асинхронно.
        Task<ProductDTO> GetByNameAsync(string name);

        // Обновляет информацию о продукте асинхронно.
        Task UpdateAsync(ProductDTO productDTO);

        // Удаляет продукт асинхронно по идентификатору.
        Task DeleteAsync(int id);
    }
}
