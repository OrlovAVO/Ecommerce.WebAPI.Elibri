using Elibri.EF.DTOS;
using Elibri.EF.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace Elibri.Core.Features.OrderServices
{
    public class OrderService : IOrderService
    {

        private readonly Context _context;

        public OrderService(Context context)
        {
            _context = context;
        }

        public async Task<List<OrderDTO>> GetAllAsync()
        {

            return await _context.Orders
                .Select(order => new OrderDTO
                {
                    OrderId = order.OrderId,
                    UserId = order.UserId,
                }).ToListAsync();
        }

        public async Task<OrderDTO> GetByIdAsync(int id)
        {

            var order = await _context.Orders.FindAsync(id);
            if (order == null) return null;

            return new OrderDTO
            {
                OrderId = order.OrderId,
                UserId = order.UserId,

            };
        }

        public async Task<OrderDTO> CreateAsync(OrderDTO orderDTO)
        {

            var order = new Order
            {
                UserId = orderDTO.UserId,

            };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            orderDTO.OrderId = order.OrderId;
            return orderDTO;
        }

        public async Task UpdateAsync(OrderDTO orderDTO)
        {

            var order = await _context.Orders.FindAsync(orderDTO.OrderId);
            if (order == null) return;

            order.UserId = orderDTO.UserId;


            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {

            var order = await _context.Orders.FindAsync(id);
            if (order == null) return;

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
