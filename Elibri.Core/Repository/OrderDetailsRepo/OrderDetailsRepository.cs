using Elibri.EF.Models;
using Elibri.Core.Repository.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Elibri.Core.Repository.OrderDetailsRepo
{
    public class OrderDetailsRepository : GenericRepository<OrderDetail>, IOrderDetailsRepository

    {
        Context _cont;
        public OrderDetailsRepository(Context context) : base(context)
        {
            _cont = context;
        }

        public IQueryable<OrderDetail> GetAll()
        {
            return _cont.Set<OrderDetail>().AsQueryable();
        }
    }
}
