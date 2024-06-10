using Elibri.EF.Models;
using Elibri.Core.Repository.GenericRepo;

namespace Elibri.Core.Repository.CartRepo
{
    // Репозиторий для работы с данными корзины
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        private readonly Context _context;

        public CartRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}
