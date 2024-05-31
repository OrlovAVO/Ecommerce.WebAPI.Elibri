using Elibri.EF.Models;
using Elibri.Core.Repository.GenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Core.Repository.OrderDetailsRepo

{
    public interface IOrderDetailsRepository : IGenericRepository<OrderDetail>
    {
    }
}
