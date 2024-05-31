using Elibri.EF.Models;
using Elibri.Core.Repository.GenericRepo;
using Elibri.Core.Repository.OrderRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Core.Repository.CartRepo
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        Context _cont;
        public CartRepository(Context context) : base(context)
        {
            _cont = context;
        }


    }
}
