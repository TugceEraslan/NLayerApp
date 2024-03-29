﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T:class
    {
        Task<T> GetByIdAsync(int id); // Geriye bir T yani entity döneceğim yani id ye göre bir data dön
        IQueryable<T> GetAll();

        // productRepository.where(x=>x.id>5).Orderby.ToListAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> expression);  // T bura da x=> kısmına bool ise x.id>5 ifadesinin doğruluğuna denk geliyor
        // IQueryable döndüğümüzde yazmış olduğumuz metodlar direkt veritabanına gitmez.ToList() veya ToListAsync() yazarsam veritabanına gider
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities); // Birden fazla kaydetmek için AddRangeAsync metodunu kullandım

        void Update(T entity);
        void Remove(T entity);

        void RemoveRange(IEnumerable<T> entities);

    }
}
