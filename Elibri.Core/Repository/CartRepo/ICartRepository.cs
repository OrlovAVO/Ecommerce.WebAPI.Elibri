using Elibri.Core.Repository.GenericRepo;
using Elibri.EF.Models;

namespace Elibri.Core.Repository.CartRepo
{
    // Интерфейс для взаимодействия с данными корзины
    public interface ICartRepository : IGenericRepository<Cart>
    {
    }
}
