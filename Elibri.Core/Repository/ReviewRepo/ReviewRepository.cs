using Elibri.EF.Models;
using Elibri.Core.Repository.CartRepo;
using Elibri.Core.Repository.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
