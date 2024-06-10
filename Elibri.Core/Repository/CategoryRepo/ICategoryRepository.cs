using Elibri.Core.Repository.GenericRepo;
using Elibri.EF.Models;

namespace Elibri.Core.Repository.CategoryRepo
{
    // Определение интерфейса ICategoryRepository расширяет обобщенный интерфейс IGenericRepository<Category>.
    // Этот интерфейс задает контракты для работы с данными категорий.
    public interface ICategoryRepository : IGenericRepository<Category>
    {
    }
}
