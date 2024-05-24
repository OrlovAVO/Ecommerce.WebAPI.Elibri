using Elibri.Context;
using Elibri.Models;
using Elibri.Repositories.CartRepo;
using Elibri.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Repositories.ReviewRepo
{
    public class ReviewRepository : IReviewRepository
    {
        Context.Context _cont;

        public ReviewRepository(Context.Context context) : base(context)
        {

        }
    }
}
