using AutoMapper;
using Elibri.DTOs.DTOS;
using Elibri.Models;
using Elibri.Repositories.OrderRepo;
using Elibri.Services.GenericServices;

namespace Elibri.Services.OrderServices
{
    public class OrderService : GenericService<Order, OrderDTO>, IOrderService
    {
        public OrderService(IOrderRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
        }



    }
}
