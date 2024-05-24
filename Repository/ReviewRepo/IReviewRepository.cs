using Elibri.Models;
using Elibri.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Repositories.ReviewRepo
{
    public class IReviewRepository
    {
        public IReviewRepository(Context.Context context)
        {
            Context = context;
        }

        public Context.Context Context { get; }
    }
}
