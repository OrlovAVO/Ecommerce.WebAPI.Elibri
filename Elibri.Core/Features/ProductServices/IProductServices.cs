using System.Collections.Generic;
using System.Threading.Tasks;
using Elibri.EF.DTOS;

namespace Elibri.Core.Features.ProductServices
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(int id);
        Task<ProductDTO> CreateAsync(ProductDTO productDTO);
        Task<List<ProductDTO>> GetProductsByCategoryIdAsync(int categoryId);
        Task<List<ProductDTO>> FilterProductsAsync(int? maxDeliveryDays, bool sortByPriceDescending, string searchTerm);
        Task<List<ProductWithRelatedDTO>> GetRelatedProductsAsync(int productId);
        Task<ProductDTO> GetByNameAsync(string name);
        Task<ProductWithRelatedDTO> GetProductWithRelatedAsync(int productId); // Добавленный метод
        Task UpdateAsync(ProductDTO productDTO);
        Task DeleteAsync(int id);
    }
}
