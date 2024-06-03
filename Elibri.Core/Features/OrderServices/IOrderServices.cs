using Elibri.EF.DTOS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Elibri.Core.Features.OrderServices
{
    public interface IOrderService
    {
        Task<List<OrderDTO>> GetAllAsync();
        Task<OrderDTO> GetByIdAsync(int id);
        Task<ServiceResult<OrderDTO>> CreateOrderAsync(CreateOrderDTO createOrderDto);
        Task DeleteAsync(int id);
    }
}
