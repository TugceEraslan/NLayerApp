using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategory();  //Geriye Task döneceğim async bir işlem olsun.Herhangi bir parametre vermeyeceğim


    }
}
