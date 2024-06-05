using Elibri.EF.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elibri.Core.Features.ProductServices
{
    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(int id);
        Task<ProductDTO> CreateAsync(ProductDTO productDTO);
        Task<List<ProductDTO>> GetProductsByCategoryIdAsync(int categoryId);
        Task UpdateAsync(ProductDTO productDTO);
        Task DeleteAsync(int id);
        Task<ProductDTO> GetByNameAsync(string name);
        Task<List<ProductDTO>> FilterProductsAsync(int? maxDeliveryDays, bool sortByPriceDescending, string searchTerm);
    }
}
