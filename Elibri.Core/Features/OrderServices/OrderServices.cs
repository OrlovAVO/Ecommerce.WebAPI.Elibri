﻿using AutoMapper;
using Elibri.EF.DTOS;
using Elibri.EF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Elibri.Core.Features.OrderServices
{
    public class OrderService : IOrderService
    {
        private readonly Context _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public OrderService(Context context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        public async Task<List<OrderDTO>> GetAllAsync()
        {
            var orders = await _context.Orders
                .Include(order => order.OrderDetails)
                .ThenInclude(od => od.Product)
                .ToListAsync();

            return _mapper.Map<List<OrderDTO>>(orders);
        }

        public async Task<OrderDTO> GetByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<ServiceResult<OrderDTO>> CreateOrderAsync(CreateOrderDTO orderDto)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (userId == null)
            {
                return new ServiceResult<OrderDTO> { IsSuccess = false, ErrorMessage = "User not authenticated." };
            }

            // Создаем новый заказ
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.UtcNow,
                FirstName = orderDto.FirstName,
                LastName = orderDto.LastName,
                Address = orderDto.Address,
                PhoneNumber = orderDto.PhoneNumber,
                CardNumber = orderDto.CardNumber
            };

            _context.Orders.Add(order);

            decimal totalAmount = 0;

            foreach (var cartItem in orderDto.CartItems)
            {
                var product = await _context.Products.FindAsync(cartItem.ProductId);
                if (product == null)
                {
                    return new ServiceResult<OrderDTO> { IsSuccess = false, ErrorMessage = $"Product with ID {cartItem.ProductId} not found." };
                }

                if (product.StockQuantity < cartItem.Quantity)
                {
                    return new ServiceResult<OrderDTO> { IsSuccess = false, ErrorMessage = $"Not enough stock for product with ID {cartItem.ProductId}." };
                }

                product.StockQuantity -= cartItem.Quantity;

                var unitPrice = product.Price;
                var itemTotal = unitPrice * cartItem.Quantity;

                totalAmount += itemTotal;

                // Создаем OrderDetail для текущего CartItemDTO
                var orderDetail = new OrderDetail
                {
                    Order = order,
                    ProductId = cartItem.ProductId,
                    StockQuantity = cartItem.Quantity,
                    TotalAmount = itemTotal
                };

                _context.OrderDetails.Add(orderDetail);
            }

            order.TotalAmount = totalAmount;

            await _context.SaveChangesAsync();

            return new ServiceResult<OrderDTO> { IsSuccess = true, Data = _mapper.Map<OrderDTO>(order) };
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }

    public class ServiceResult<T>
    {
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
        public T Data { get; set; }
    }
}