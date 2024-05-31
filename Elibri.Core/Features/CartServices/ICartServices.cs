using Elibri.EF.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elibri.Core.Features.CartServices
{
    public interface ICartService
    {
        Task<List<CartDTO>> GetAllAsync();
        Task<CartDTO> GetByIdAsync(int id);
        Task<CartDTO> CreateAsync(CartDTO cartDTO);
        Task UpdateAsync(CartDTO cartDTO);
        Task DeleteAsync(int id);
    }
}
