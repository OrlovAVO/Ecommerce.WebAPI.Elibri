using Elibri.EF.Models;

namespace Elibri.Core.Repository.ProductRepo
{
    // Интерфейс IProductRepository определяет контракт для работы с продуктами.
    public interface IProductRepository
    {
        // Метод для получения списка всех продуктов с пагинацией.
        Task<List<Product>> GetAllAsync(int pageNumber, int pageSize);

        // Метод для получения продукта по его идентификатору.
        Task<Product> GetByIdAsync(int id);

        // Метод для создания нового продукта.
        Task CreateAsync(Product product);

        // Метод для обновления существующего продукта.
        Task UpdateAsync(Product product);

        // Метод для удаления продукта по его идентификатору.
        Task DeleteAsync(int id);

        // Метод для получения продукта по его имени.
        Task<Product> GetByNameAsync(string name);

        // Метод для получения списка продуктов по идентификатору категории с пагинацией.
        Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId, int pageNumber, int pageSize);

        // Метод для фильтрации продуктов с учетом различных параметров с пагинацией.
        Task<(List<Product> Items, int TotalItems)> FilterProductsAsync(
            int? categoryId,
            int? maxDeliveryDays,
            string sortOrder,
            string searchTerm,
            int pageNumber,
            int pageSize);

        // Метод для подсчета отфильтрованных продуктов с учетом различных параметров.
        Task<int> CountFilteredProductsAsync(
            int? categoryId,
            int? maxDeliveryDays,
            string searchTerm);

        // Метод для подсчета общего числа продуктов.
        Task<int> CountAsync();

        // Метод для подсчета числа продуктов в определенной категории.
        Task<int> CountByCategoryAsync(int categoryId);
    }
}
