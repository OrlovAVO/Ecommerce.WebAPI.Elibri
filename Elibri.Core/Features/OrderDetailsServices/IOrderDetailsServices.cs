using Elibri.Core.Features.GenericServices;
using Elibri.EF.DTOS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Core.Features.OrderDetailsServices
{
    public interface IOrderDetailService : IGenericService<OrderDetailDTO> 
    {
        Task<List<OrderDetailDTO>> GetOrderDetailsByUserIdAsync(string userId);
    }
}
