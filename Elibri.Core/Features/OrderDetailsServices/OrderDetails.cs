using Elibri.EF.DTOS;
using Elibri.Core.Repository.OrderDetailsRepo;
using Elibri.EF.Models;
using Elibri.Core.Features.GenericServices;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Elibri.Core.Features.OrderDetailsServices
{
    public class OrderDetailService : GenericService<OrderDetail, OrderDetailDTO>, IOrderDetailService
    {
        private readonly IOrderDetailsRepository _repository;
        private readonly IMapper _mapper;

        public OrderDetailService(IOrderDetailsRepository repository, IMapper mapper)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<OrderDetailDTO>> GetOrderDetailsByUserIdAsync(string userId)
        {
            var orderDetails = await _repository.GetAll()
                .Include(od => od.Order)
                .Where(od => od.Order.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<OrderDetailDTO>>(orderDetails);
        }
    }
}
