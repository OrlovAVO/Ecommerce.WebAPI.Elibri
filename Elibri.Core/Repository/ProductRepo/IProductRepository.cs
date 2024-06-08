using System.Collections.Generic;
using System.Threading.Tasks;
using Elibri.EF.Models;

namespace Elibri.Core.Repository.ProductRepo
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllAsync(int pageNumber, int pageSize);
        Task<Product> GetByIdAsync(int id);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task<Product> GetByNameAsync(string name);
        Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId, int pageNumber, int pageSize);
        Task<List<Product>> FilterProductsAsync(
            int? categoryId,
            int? maxDeliveryDays,
            bool sortByPriceDescending,
            string searchTerm,
            int pageNumber,
            int pageSize);

        Task<int> CountFilteredProductsAsync(
            int? categoryId,
            int? maxDeliveryDays,
            string searchTerm);
        Task<int> CountAsync();
        Task<int> CountByCategoryAsync(int categoryId);
    }
}
