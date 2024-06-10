using Elibri.Core.Features.GenericServices;
using Elibri.EF.DTOS;

namespace Elibri.Core.Features.OrderDetailsServices
{
    // Интерфейс сервиса для работы с деталями заказов.
    public interface IOrderDetailService : IGenericService<OrderDetailDTO>
    {
        // Получает детали заказов по идентификатору пользователя асинхронно.
        Task<List<OrderDetailDTO>> GetOrderDetailsByUserIdAsync(string userId);
    }
}
