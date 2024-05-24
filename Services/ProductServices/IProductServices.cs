using Elibri.DTOs.DTOS;
using Elibri.Services.GenericServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Services.ProductServices
{

    public interface IProductService
    {
        Task<List<ProductDTO>> GetAllAsync();
        Task<ProductDTO> GetByIdAsync(int id);
        Task<ProductDTO> CreateAsync(ProductDTO productDTO);
        Task<List<ProductDTO>> GetProductsByCategoryIdAsync(int categoryId);
        Task UpdateAsync(ProductDTO productDTO);
        Task DeleteAsync(int id);
/*        Task<List<ReviewDTO>> GetReviewsByProductIdAsync(int productId);
        Task<ReviewDTO> AddReviewAsync(ReviewDTO reviewDTO);*/
    }

}