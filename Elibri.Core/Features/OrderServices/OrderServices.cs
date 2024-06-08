using AutoMapper;
using Elibri.EF.DTOS;
using Elibri.EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Elibri.Core.Features.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly Context _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(Context context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<OrderDTO>> GetAllAsync()
        {
            var orders = await _context.Orders
                .Include(order => order.OrderDetails)
                    .ThenInclude(od => od.Product)
                .ToListAsync();

            return orders.Select(order =>
            {
                var orderDto = _mapper.Map<OrderDTO>(order);

                // Даты уже будут в нужном формате благодаря свойствам OrderDateFormatted и DeliveryDateFormatted
                foreach (var cartItem in orderDto.CartItems)
                {
                    var product = order.OrderDetails.FirstOrDefault(od => od.ProductId == cartItem.ProductId)?.Product;
                    if (product != null)
                    {
                        cartItem.Image = product.Image;
                    }
                }

                return orderDto;
            }).ToList();
        }

        public async Task<ServiceResult<OrderDTO>> CreateOrderAsync(CreateOrderDTO createOrderDto)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return new ServiceResult<OrderDTO> { IsSuccess = false, ErrorMessage = "Пользователь не аутентифицирован." };
            }

            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                FirstName = createOrderDto.FirstName,
                LastName = createOrderDto.LastName,
                Address = createOrderDto.Address,
                PhoneNumber = createOrderDto.PhoneNumber,
                CardNumber = createOrderDto.CardNumber,
                Status = "В обработке" 
            };

            _context.Orders.Add(order);

            decimal totalAmount = 0;
            DateTime maxDeliveryDate = DateTime.UtcNow;

            foreach (var cartItem in createOrderDto.CartItems)
            {
                var product = await _context.Products.FindAsync(cartItem.ProductId);
                if (product == null)
                {
                    return new ServiceResult<OrderDTO> { IsSuccess = false, ErrorMessage = $"Продукт с ID:{cartItem.ProductId} не найден." };
                }

                if (product.StockQuantity < cartItem.Quantity)
                {
                    return new ServiceResult<OrderDTO> { IsSuccess = false, ErrorMessage = $"Недостаточно запасов для продукта с ID:{cartItem.ProductId}." };
                }

                product.StockQuantity -= cartItem.Quantity;

                var unitPrice = product.Price;
                var itemTotal = unitPrice * cartItem.Quantity;

                totalAmount += itemTotal;

                var orderDetail = new OrderDetail
                {
                    Order = order,
                    ProductId = cartItem.ProductId,
                    StockQuantity = cartItem.Quantity,
                    TotalPrice = itemTotal
                };

                _context.OrderDetails.Add(orderDetail);

                var deliveryDate = DateTime.UtcNow.AddDays(product.DeliveryDays);
                if (deliveryDate > maxDeliveryDate)
                {
                    maxDeliveryDate = deliveryDate;
                }
            }

            order.TotalPrice = totalAmount;
            order.DeliveryDate = maxDeliveryDate; 

            await _context.SaveChangesAsync();

            var orderDTO = _mapper.Map<OrderDTO>(order);

           
            return new ServiceResult<OrderDTO> { IsSuccess = true, Data = orderDTO };
        }



        public async Task<List<OrderDTO>> GetOrdersByUserIdAsync(string userId)
        {
            var orders = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .Where(o => o.UserId == userId)
                .ToListAsync();

            var orderDTOs = orders.Select(order =>
            {
                var orderDto = _mapper.Map<OrderDTO>(order);

                // Даты уже будут в нужном формате благодаря свойствам OrderDateFormatted и DeliveryDateFormatted
                foreach (var cartItem in orderDto.CartItems)
                {
                    var product = order.OrderDetails.FirstOrDefault(od => od.ProductId == cartItem.ProductId)?.Product;
                    if (product != null)
                    {
                        cartItem.Image = product.Image;
                    }
                }

                return orderDto;
            }).ToList();

            return orderDTOs;
        }


        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }



    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}


