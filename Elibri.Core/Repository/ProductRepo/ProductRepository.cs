using Elibri.EF.Models;
using Microsoft.EntityFrameworkCore;

namespace Elibri.Core.Repository.ProductRepo
{
    public class ProductRepository : IProductRepository
    {
        private readonly Context _context;

        // Конструктор класса, принимающий контекст базы данных.
        public ProductRepository(Context context)
        {
            _context = context;
        }

        // Метод для получения списка всех продуктов с пагинацией.
        public async Task<List<Product>> GetAllAsync(int pageNumber, int pageSize)
        {
            return await _context.Products
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // Метод для получения продукта по его идентификатору.
        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        // Метод для создания нового продукта.
        public async Task CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        // Метод для обновления существующего продукта.
        public async Task UpdateAsync(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Метод для удаления продукта по его идентификатору.
        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }

        // Метод для получения продукта по его имени.
        public async Task<Product> GetByNameAsync(string name)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name == name);
        }

        // Метод для получения списка продуктов по идентификатору категории с пагинацией.
        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId, int pageNumber, int pageSize)
        {
            return await _context.Products
                .Where(p => p.CategoryId == categoryId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        // Метод для фильтрации продуктов с учетом различных параметров с пагинацией.
        public async Task<(List<Product>, int)> FilterProductsAsync(
            int? categoryId,
            int? maxDeliveryDays,
            string sortOrder,
            string searchTerm,
            int pageNumber,
            int pageSize)
        {
            // Создание запроса для фильтрации продуктов
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

            // Счетчик общего числа элементов до применения пагинации
            int totalItems = await query.CountAsync();

            // Применение сортировки
            switch (sortOrder?.ToLower())
            {
                case "cheapest":
                    query = query.OrderBy(p => p.Price);
                    break;
                case "mostexpensive":
                    query = query.OrderByDescending(p => p.Price);
                    break;
                default:
                    // Если sortOrder не задан или не соответствует ожидаемым значениям, сортировка не применяется.
                    break;
            }

            // Применение пагинации
            query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            // Получение отфильтрованных и отсортированных элементов
            List<Product> items = await query.ToListAsync();

            // Возврат списка элементов и общего числа
            return (items, totalItems);
        }

        // Метод для подсчета отфильтрованных продуктов с учетом различных параметров.
        public async Task<int> CountFilteredProductsAsync(int? categoryId, int? maxDeliveryDays, string searchTerm)
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

        // Метод для подсчета общего числа продуктов.
        public async Task<int> CountAsync()
        {
            return await _context.Products.CountAsync();
        }

        // Метод для подсчета числа продуктов в определенной категории.
        public async Task<int> CountByCategoryAsync(int categoryId)
        {
            return await _context.Products.Where(p => p.CategoryId == categoryId).CountAsync();
        }
    }
}
