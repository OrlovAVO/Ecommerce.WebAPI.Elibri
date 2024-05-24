using Elibri.Context;
using Elibri.Models;
using Elibri.Repositories.GenericRepo;

namespace Elibri.Repositories.CategoryRepo
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        Context.Context _cont;
        public CategoryRepository(Context.Context context) : base(context)
        {
            _cont = context;
        }
    }
}
