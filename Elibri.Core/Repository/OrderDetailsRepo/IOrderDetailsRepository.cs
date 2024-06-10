using Elibri.Core.Repository.GenericRepo;
using Elibri.EF.Models;

namespace Elibri.Core.Repository.OrderDetailsRepo
{
    // Интерфейс IOrderDetailsRepository наследует интерфейс IGenericRepository<OrderDetail> и добавляет дополнительный метод GetAll.
    public interface IOrderDetailsRepository : IGenericRepository<OrderDetail>
    {
        IQueryable<OrderDetail> GetAll(); // Получить все детали заказа в виде запроса LINQ.
    }
}
