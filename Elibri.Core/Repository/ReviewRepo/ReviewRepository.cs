using Elibri.EF.Models;

namespace Elibri.Core.Repository.ReviewRepo
{
    public class ReviewRepository : IReviewRepository
    {
        Context _cont;

        public ReviewRepository(Context context) : base(context)
        {

        }
    }
}
