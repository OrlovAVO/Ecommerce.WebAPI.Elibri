using Elibri.EF.Models;
using Elibri.Core.Repository.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Core.Repository.ProductRepo
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        Context _cont;
        public ProductRepository(Context context) : base(context)
        {
            _cont = context;
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _cont.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Product> GetByNameAsync(string name)
        {
            return await _cont.Products.FirstOrDefaultAsync(p => p.Name == name);
        }

    }
}
