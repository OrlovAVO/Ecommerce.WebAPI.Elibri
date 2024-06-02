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
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(Context context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<OrderDTO>> GetAllAsync()
        {
            return await _context.Orders
                .Include(order => order.OrderDetails)
                .ThenInclude(od => od.Product)
                .Select(order => new OrderDTO
                {
                    OrderId = order.OrderId,
                    UserId = order.UserId,
                    FirstName = order.FirstName,
                    LastName = order.LastName,
                    Address = order.Address,
                    PhoneNumber = order.PhoneNumber,
                    CardNumber = order.CardNumber,
                    CartItems = order.OrderDetails.Select(od => new CartItemDTO
                    {
                        ProductId = od.ProductId,
                        Quantity = od.StockQuantity
                    }).ToList()
                }).ToListAsync();
        }

        public async Task<OrderDTO> GetByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null) return null;

            return new OrderDTO
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                FirstName = order.FirstName,
                LastName = order.LastName,
                Address = order.Address,
                PhoneNumber = order.PhoneNumber,
                CardNumber = order.CardNumber,
                CartItems = order.OrderDetails.Select(od => new CartItemDTO
                {
                    ProductId = od.ProductId,
                    Quantity = od.StockQuantity
                }).ToList()
            };
        }

        public async Task<ServiceResult<OrderDTO>> CreateOrderAsync(CreateOrderDTO orderDto)
        {
            // Получаем UserId из токена
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Проверяем, получили ли мы UserId
            if (userId == null)
            {
                return new ServiceResult<OrderDTO> { IsSuccess = false, ErrorMessage = "User not authenticated." };
            }

            // Инициализируем общую сумму заказа
            decimal totalAmount = 0;

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
            await _context.SaveChangesAsync();

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

                var orderDetail = new OrderDetail
                {
                    OrderId = order.OrderId,
                    ProductId = cartItem.ProductId,
                    StockQuantity = cartItem.Quantity,
                    TotalAmount = itemTotal
                };

                _context.OrderDetails.Add(orderDetail);
            }

            await _context.SaveChangesAsync();

            var createdOrderDto = new OrderDTO
            {
                OrderId = order.OrderId,
                UserId = userId,
                FirstName = order.FirstName,
                LastName = order.LastName,
                Address = order.Address,
                PhoneNumber = order.PhoneNumber,
                CardNumber = order.CardNumber,
                CartItems = order.OrderDetails.Select(od => new CartItemDTO
                {
                    ProductId = od.ProductId,
                    Quantity = od.StockQuantity
                }).ToList()
            };

            return new ServiceResult<OrderDTO> { IsSuccess = true, Data = createdOrderDto };
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