using Elibri.Context;
using Elibri.Models;
using Elibri.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Repositories.OrderDetailsRepo
{
    public class OrderDetailsRepository : GenericRepository<OrderDetail>, IOrderDetailsRepository

    {
        Context.Context _cont;
        public OrderDetailsRepository(Context.Context context) : base(context)
        {
            _cont = context;
        }
    }
}
