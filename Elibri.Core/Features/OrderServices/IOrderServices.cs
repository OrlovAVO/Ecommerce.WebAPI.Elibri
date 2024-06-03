using Elibri.EF.DTOS;

namespace Elibri.Core.Features.OrderServices
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllAsync();
        Task<OrderDTO> GetByIdAsync(int id);
        Task<ServiceResult<OrderDTO>> CreateOrderAsync(CreateOrderDTO orderDto);
        Task DeleteAsync(int id);
        Task<List<OrderDTO>> GetOrdersByUserIdAsync(string userId);
    }
}
