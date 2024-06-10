using Elibri.EF.Models;
using Elibri.Core.Repository.GenericRepo;

namespace Elibri.Core.Repository.OrderRepo
{
    // Интерфейс IOrderRepository наследует интерфейс IGenericRepository<Order>.
    public interface IOrderRepository : IGenericRepository<Order>
    {
    }
}
