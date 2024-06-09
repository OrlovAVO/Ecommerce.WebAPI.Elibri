using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elibri.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Elibri.Core.Repository.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        public ProductRepository(Context context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId, int pageNumber, int pageSize)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<(List<Product>, int)> FilterProductsAsync(
            int? categoryId,
            int? maxDeliveryDays,
            string sortOrder,
            string searchTerm,
            int pageNumber,
            int pageSize)
        {
            IQueryable<Product> query = _context.Products;

            // Применяем фильтры
            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (maxDeliveryDays.HasValue)
            {
                query = query.Where(p => p.DeliveryDays <= maxDeliveryDays.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                string searchTermLower = searchTerm.ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(searchTermLower));
            }

            // Считаем общее количество элементов до применения пагинации
            int totalItems = await query.CountAsync();

            // Применяем сортировку
            switch (sortOrder?.ToLower())
            {
                case "cheapest":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "mostexpensive":
                    query = query.OrderByDescending(p => p.Price);
                    break;
                case "none":
                default:
                    // Сортировка не применяется, если sortOrder - "None" или другое значение
                    break;
            }

            // Применяем пагинацию
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            // Получаем отфильтрованные и отсортированные элементы
            List<Product> items = await query.ToListAsync();

            // Возвращаем список элементов и общее количество
            return (items, totalItems);
        }






        public async Task<int> CountFilteredProductsAsync(
            int? categoryId,
            int? maxDeliveryDays,
            string searchTerm)
        {
            IQueryable<Product> query = _context.Products;

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            if (maxDeliveryDays.HasValue)
            {
                query = query.Where(p => p.DeliveryDays <= maxDeliveryDays.Value);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(p => p.Name.Contains(searchTerm) || p.Description.Contains(searchTerm));
            }

            return await query.CountAsync();
        }

        public async Task<int> CountAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<int> CountByCategoryAsync(int categoryId)
        {
            return await _context.Products.Where(p => p.CategoryId == categoryId).CountAsync();
        }
    }
}
