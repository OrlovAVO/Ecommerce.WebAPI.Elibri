using Elibri.EF.Models;
using Elibri.Core.Repository.GenericRepo;

namespace Elibri.Core.Repository.CategoryRepo
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        Context _cont;
        public CategoryRepository(Context context) : base(context)
        {
            _cont = context;
        }
    }
}
