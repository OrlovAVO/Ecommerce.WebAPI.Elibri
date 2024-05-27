using Elibri.DTOs.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elibri.Services.OrderServices
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
