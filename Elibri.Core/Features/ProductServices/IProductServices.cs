using System.Collections.Generic;
using System.Threading.Tasks;
using Elibri.EF.DTOS;

namespace Elibri.Core.Features.ProductServices
{
    public interface IProductService
    {
        Task<PagedResult<ProductDTO>> GetAllAsync(int pageNumber, int pageSize);
        Task<ProductDTO> GetByIdAsync(int id);
        Task<ProductDTO> CreateAsync(ProductDTO productDTO);
        Task<PagedResult<ProductDTO>> GetProductsByCategoryIdAsync(int categoryId, int pageNumber = 1, int pageSize = 10);
        Task<List<ProductDTO>> FilterProductsAsync(int? maxDeliveryDays, bool sortByPriceDescending, string searchTerm, int pageNumber = 1, int pageSize = 10);
        Task<ProductWithRelatedDTO> GetProductWithRelatedAsync(int productId);
        Task<ProductDTO> GetByNameAsync(string name);
        Task UpdateAsync(ProductDTO productDTO);
        Task DeleteAsync(int id);
    }
}
