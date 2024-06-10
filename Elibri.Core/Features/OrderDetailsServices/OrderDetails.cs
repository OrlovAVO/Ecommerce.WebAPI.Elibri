using AutoMapper;
using Elibri.Core.Features.GenericServices;
using Elibri.Core.Repository.OrderDetailsRepo;
using Elibri.EF.DTOS;
using Elibri.EF.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Elibri.Core.Features.OrderDetailsServices
{
    // Сервис для работы с деталями заказов.
    public class OrderDetailService : GenericService<OrderDetail, OrderDetailDTO>, IOrderDetailService
    {
        private readonly IOrderDetailsRepository _repository;
        private readonly IMapper _mapper;
        private readonly Context _context;

        // Конструктор класса OrderDetailService.
        public OrderDetailService(IOrderDetailsRepository repository, IMapper mapper, Context context)
            : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

        // Получает детали заказов по идентификатору пользователя асинхронно.
        public async Task<List<OrderDetailDTO>> GetOrderDetailsByUserIdAsync(string userId)
        {
            var orderDetails = await _repository.GetAll()
                .Include(od => od.Order)
                .Where(od => od.Order.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<OrderDetailDTO>>(orderDetails);
        }

        // Получает все детали заказов асинхронно.
        public async Task<ActionResult<List<OrderDetailDTO>>> GetAllOrderDetails()
        {
            var orderDetails = await GetAllAsync();
            if (orderDetails == null || orderDetails.Count == 0)
            {
                return new List<OrderDetailDTO>();
            }

            var orderDetailDTOs = new List<OrderDetailDTO>();

            foreach (var od in orderDetails)
            {
                var cartItems = await _context.CartItems
                    .Where(ci => ci.CartId == od.OrderId)
                    .ToListAsync();

                var cartItemDTOs = cartItems.Select(ci => new CartItemDTO
                {
                    ProductId = ci.ProductId,
                    Quantity = ci.Quantity
                }).ToList();

                var orderDetailDTO = new OrderDetailDTO
                {
                    OrderDetailId = od.OrderDetailId,
                    OrderId = od.OrderId,
                    CartItems = cartItemDTOs,
                    TotalPrice = od.TotalPrice
                };

                orderDetailDTOs.Add(orderDetailDTO);
            }

            return orderDetailDTOs;
        }
    }
}
