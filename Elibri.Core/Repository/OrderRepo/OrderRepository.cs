using Elibri.EF.Models;
using Elibri.Core.Repository.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Core.Repository.OrderRepo
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        Context _cont;
        public OrderRepository(Context context) : base(context)
        {
            _cont = context;
        }


    }
}
