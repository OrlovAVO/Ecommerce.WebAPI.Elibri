using Elibri.EF.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elibri.Core.Features.OrderServices
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllAsync();
        Task<ServiceResult<OrderDTO>> CreateOrderAsync(CreateOrderDTO orderDto);
        Task DeleteAsync(int id);
        Task<List<OrderDTO>> GetOrdersByUserIdAsync(string userId);
    }
}
