using Elibri.Core.Repository.GenericRepo;
using Elibri.EF.Models;

namespace Elibri.Core.Repository.OrderRepo
{
    // Класс OrderRepository наследует от GenericRepository<Order> и реализует интерфейс IOrderRepository.
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly Context _cont;

        // Конструктор класса, принимающий контекст базы данных.
        public OrderRepository(Context context) : base(context)
        {
            _cont = context;
        }
    }
}
