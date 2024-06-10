using Elibri.Core.Repository.GenericRepo;
using Elibri.EF.Models;

namespace Elibri.Core.Repository.OrderDetailsRepo
{
    // Класс OrderDetailsRepository реализует интерфейс IOrderDetailsRepository и наследует класс GenericRepository<OrderDetail>.
    public class OrderDetailsRepository : GenericRepository<OrderDetail>, IOrderDetailsRepository
    {
        private readonly Context _cont;

        // Конструктор класса, принимающий экземпляр контекста базы данных.
        public OrderDetailsRepository(Context context) : base(context)
        {
            _cont = context;
        }

        // Метод GetAll возвращает все детали заказа в виде запроса LINQ.
        public IQueryable<OrderDetail> GetAll()
        {
            return _cont.Set<OrderDetail>().AsQueryable();
        }
    }
}
