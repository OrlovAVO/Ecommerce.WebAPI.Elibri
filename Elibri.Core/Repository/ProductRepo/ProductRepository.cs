using Elibri.Core.Repository.GenericRepo;
using Elibri.EF.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elibri.Core.Repository.ProductRepo
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<List<Product>> FilterProductsAsync(int? maxDeliveryDays, bool sortByPriceDescending, string searchTerm)
        {
            IQueryable<Product> query = _context.Products;

            if (maxDeliveryDays.HasValue)
            {
                query = query.Where(p => p.DeliveryDays <= maxDeliveryDays.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()));
            }

            query = sortByPriceDescending ? query.OrderByDescending(p => p.Price) : query.OrderBy(p => p.Price);

            return await query.ToListAsync();
        }
    }
}
