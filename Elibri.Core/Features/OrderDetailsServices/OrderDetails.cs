using Elibri.EF.DTOS;
using Elibri.Core.Repository.GenericRepo;
using Elibri.Core.Features.OrderServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Elibri.Core.Repository.OrderDetailsRepo;
using Elibri.EF.Models;
using Elibri.Core.Features.GenericServices;
using AutoMapper;

namespace Elibri.Core.Features.OrderDetailsServices
{
    public class OrderDetailService : GenericService<OrderDetail, OrderDetailDTO>, IOrderDetailService
    {
        public OrderDetailService(IOrderDetailsRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
