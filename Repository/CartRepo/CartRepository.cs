using Elibri.Context;
using Elibri.Models;
using Elibri.Repositories.GenericRepo;
using Elibri.Repositories.OrderRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Repositories.CartRepo
{
    public class CartRepository : GenericRepository<Cart>, ICartRepository
    {
        Context.Context _cont;
        public CartRepository(Context.Context context) : base(context)
        {
            _cont = context;
        }


    }
}
