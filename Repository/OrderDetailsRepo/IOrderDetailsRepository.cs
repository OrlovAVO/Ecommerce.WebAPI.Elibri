using Elibri.Models;
using Elibri.Repositories.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Repositories.OrderDetailsRepo

{
    public interface IOrderDetailsRepository : IGenericRepository<OrderDetail>
    {
    }
}
