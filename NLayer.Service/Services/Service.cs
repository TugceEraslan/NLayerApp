using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Service.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Services
{
    public class Service<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> _repository;  // Veritabanı ile ilişkisi olması lazım
        private readonly IUnitOfWork _unitOfWork;

        public Service(IGenericRepository<T> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<T> AddAsync(T entity)  // Geriye eklenen entity T tipinde dön
        {
            await _repository.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            return entity;
        }

        public async Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities)
        {
            await _repository.AddRangeAsync(entities);
            await _unitOfWork.CommitAsync();
            return entities;
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> expression)
        {
            return await _repository.AnyAsync(expression);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _repository.GetAll().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var hasProduct= await _repository.GetByIdAsync(id);

            if (hasProduct == null)
            {
                throw new NotFoundException($"{typeof(T).Name}({id}) Not Found");

            }

            return hasProduct;

        }

        public async Task RemoveAsync(T entity)
        {
            _repository.Remove(entity);  
            await _unitOfWork.CommitAsync(); // Repository de async olmuyordu ama service te SaveChange işlmei gerçekleştireceğimiz için.Önce sileceğimiz entity i işaretledik sonra da _unitOfWork.CommitAsync() dedik
        }

        public async Task RemoveRangeAsync(IEnumerable<T> entities)
        {
            _repository.RemoveRange(entities);
             await _unitOfWork.CommitAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _repository.Update(entity); 
            await _unitOfWork.CommitAsync();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> expression)  // ToList() değil IQueryable<T> dönüyor. ToList() ya da ToListAsync() i where metodunu çağırdığı yerde yani API da kullanacağız
        {
            return _repository.Where(expression);
        }
    }
}
