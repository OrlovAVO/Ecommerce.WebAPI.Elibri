using Elibri.EF.DTOS;

namespace Elibri.Core.Features.OrderServices
{
    // Интерфейс сервиса для работы с заказами.
    public interface IOrderService
    {
        // Получает все заказы асинхронно.
        Task<List<OrderDTO>> GetAllAsync();

        // Создает заказ асинхронно.
        Task<ServiceResult<OrderDTO>> CreateOrderAsync(CreateOrderDTO orderDto);

        // Удаляет заказ асинхронно по идентификатору.
        Task DeleteAsync(int id);

        // Получает заказы пользователя по идентификатору пользователя асинхронно.
        Task<List<OrderDTO>> GetOrdersByUserIdAsync(string userId);
    }
}
