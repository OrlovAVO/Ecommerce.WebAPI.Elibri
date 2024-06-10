using Elibri.EF.Models;

namespace Elibri.Core.Repository.ReviewRepo
{
    public class IReviewRepository
    {
        // Конструктор класса IReviewRepository.
        public IReviewRepository(Context context)
        {
            Context = context;
        }

        // Свойство для доступа к контексту базы данных.
        public Context Context { get; }
    }
}
