using Elibri.Context;
using Elibri.Models;
using Elibri.Repositories.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Elibri.Repositories.ProductRepo
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        Context.Context _cont;
        public ProductRepository(Context.Context context) : base(context)
        {
            _cont = context;
        }

        public async Task<List<Product>> GetProductsByCategoryIdAsync(int categoryId)
        {
            return await _cont.Products
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

    }
}
