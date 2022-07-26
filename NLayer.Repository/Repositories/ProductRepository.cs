using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using NLayer.Core.Repositories;

namespace NLayer.Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    // ProductRepository , GenericRepository den miras alsın ayrıca IProductRepository interface inden eksta metodum gelsin. GenericRepository de olan her şey ProductRepository de de olacak
    {
        public ProductRepository(AppDbContext context) : base(context) // context i ProductRepository de tekrar oluşturduk çünkü GenericRepository de protected olarak tanımlıydı
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            // Include ile Eager Loading yaptım. Yani data yı çekerken kategorilerinin de alınmasını istedim
            return await _context.Products.Include(x => x.Category).ToListAsync();  // Include dedikten sonra dahil etmek istediğim yani bağlı olduğu Category i seçiyorum

        }
    }
}
