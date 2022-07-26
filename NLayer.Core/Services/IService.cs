using System.Linq.Expressions;

namespace NLayer.Core.Services
{
    public interface IService<T> where T : class
    {
        Task<T> GetByIdAsync(int id); // Geriye bir T yani entity döneceğim yani id ye göre bir data dön
        Task<IEnumerable<T>> GetAllAsync();  // Tüm datayı çekelim

        // productRepository.where(x=>x.id>5).Orderby.ToListAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);  // T bura da x=> kısmına bool ise x.id>5 ifadesinin doğruluğuna denk geliyor
        // IQueryable döndüğümüzde yazmış olduğumuz metodlar direkt veritabanına gitmez.ToList() veya ToListAsync() yazarsam veritabanına gider
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities); // Birden fazla kaydetmek için AddRangeAsync metodunu kullandım

        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);

        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
