using Elibri.EF.Models;
using Elibri.Core.Repository.GenericRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elibri.Core.Repository.ProductRepo
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId);
        Task<Product> GetByNameAsync(string name);
        Task<List<Product>> FilterProductsAsync(int? maxDeliveryDays, bool sortByPriceDescending, string searchTerm);
    }
}
