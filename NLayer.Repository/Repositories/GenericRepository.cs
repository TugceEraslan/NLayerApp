using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class  // T nin class olduğunu belirtmem lazım
    {
        protected readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;  // readonly key ini kullanmamızın sebebi _context ve _dbSet değişkenlerinin set edilmesini önlemek için

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet =_context.Set<T>();
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsNoTracking().AsQueryable();
            // AsNoTracking() ile henüz çektiğimiz dataları memory e almadığımız için daha performanslı çalışıyor
            // AsQueryable(); ise henüz ToList() yapmadığımız için order by yapabilirim
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id); // FindAsync() metodu benden tabloda ki primary key alanı bekler
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);  // Aslında Remove komutu ile veritabanından silme işlemi yapmıyoruz.Entity nin state ini değiştiriyoruz
         // _context.Entry(entity).State = EntityState.Deleted; yapmak ile aynı yukardaki işlem
        }

        public void RemoveRange(IEnumerable<T> entities)   
                                                           
        {
            _dbSet.RemoveRange(entities); // foreach ile entity lerde dönüp her bir entity nin state ini deleted olarak işaretliyor
                                          // ne zaman SaveChanges() yaparsam işaretlediği deleted flagli entityleri veritabanından siliyor
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)
        {
            return _dbSet.Where(expression);
        }
    }
}
