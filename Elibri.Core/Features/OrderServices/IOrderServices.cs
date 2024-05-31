using Elibri.EF.DTOS;
using Elibri.EF.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elibri.Core.Features.OrderServices
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllAsync();
        Task<OrderDTO> GetByIdAsync(int id);
        Task<OrderDTO> CreateAsync(OrderDTO orderDTO);
        Task UpdateAsync(OrderDTO orderDTO);
        Task DeleteAsync(int id);
    }
}
