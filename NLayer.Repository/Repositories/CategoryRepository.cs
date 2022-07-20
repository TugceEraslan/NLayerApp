using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Category> GetSingleCategoryByIdWithProductsAsync(int categoryId)
        {
            return await _context.Categories.Include(x => x.Products).Where(x => x.Id == categoryId).SingleOrDefaultAsync();  // _context üzerinden kategorilere git ve Include ile bu kategorilerin bağlı olduğu product larıda dahil et
                                                                                                                             // Category nin birden fazla products ı olabilir x => x.Products
                                                                                                                             // SingleOrDefaultAsync() ın , FirstOrDefaultAsync() ın farkı birden fazla id den gelirse FirstOrDefault gibi ilk gelen id yi almaz aksine id tek olması gerektiği için birden fazla id gelirse hata fırlatır
        }
    }
}
