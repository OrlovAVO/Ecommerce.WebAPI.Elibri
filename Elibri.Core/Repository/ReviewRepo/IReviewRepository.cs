using Elibri.EF.Models;
using Elibri.Core.Repository.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Core.Repository.ReviewRepo
{
    public class IReviewRepository
    {
        public IReviewRepository(Context context)
        {
            Context = context;
        }

        public Context Context { get; }
    }
}
