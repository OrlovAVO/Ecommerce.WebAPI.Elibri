using Elibri.EF.Models;
using Elibri.Core.Repository.GenericRepo;

namespace Elibri.Core.Repository.CategoryRepo
{
    // Репозиторий для работы с данными категорий
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly Context _context;

        public CategoryRepository(Context context) : base(context)
        {
            _context = context;
        }
    }
}
