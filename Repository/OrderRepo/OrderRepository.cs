using Elibri.Context;
using Elibri.Models;
using Elibri.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Repositories.OrderRepo
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        Context.Context _cont;
        public OrderRepository(Context.Context context) : base(context)
        {
            _cont = context;
        }


    }
}
