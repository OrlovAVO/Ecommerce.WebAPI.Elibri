using AutoMapper;
using Elibri.DTOs.DTOS;
using Elibri.Models;
using Elibri.Repositories.OrderDetailsRepo;
using Elibri.Services.GenericServices;
using Elibri.Repositories.GenericRepo;
using Elibri.Services.OrderServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Services.OrderDetailsServices
{
    public class OrderDetailService : GenericService<OrderDetail, OrderDetailDTO>, IOrderDetailService
    {
        public OrderDetailService(IOrderDetailsRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }
    }
}
