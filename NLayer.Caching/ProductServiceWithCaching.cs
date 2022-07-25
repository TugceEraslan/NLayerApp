using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
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

namespace NLayer.Caching
{
    public class ProductServiceWithCaching : IProductService
    {
        private const string CacheProductKey = "productsCache";
        private readonly IMapper _mapper;  // CustomResponseDto() nesenlerim olduğu için IMapper ı kullanmam gerekiyor
        private readonly IMemoryCache _memoryCache;
        private readonly IProductRepository _repository;
        private readonly IUnitOfWork _unitOfWork;  // Db ye yansıtmak istediğim için

        public ProductServiceWithCaching(IUnitOfWork unitOfWork, IProductRepository repository, IMemoryCache memoryCache, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
            _memoryCache = memoryCache;
            _mapper = mapper;

            // İlk nesne örneği oluşturulduğu anda bir cache leme yapmam gerekiyor

            if(!_memoryCache.TryGetValue(CacheProductKey, out _))  // Eğer yok ise _memoryCache den TryGetValue() ile değeri almaya çalış. Geriye bool döner ve değeri döner. out _ ile değer dönmesini istemiyoruz. bize sadece true,false dönse yeter
            {
                _memoryCache.Set(CacheProductKey, _repository.GetProductsWithCategory().Result);  // Eğer uygulama ilk ayağa kalktığında cache yoksa bu satırla ilk cachelememizi yaparız
            }
        }

        public async Task<Product> AddAsync(Product entity)
        {
           await _repository.AddAsync(entity);  // _repository üzerinden AddAsync i çağırıp entity i ekliyorum
           await _unitOfWork.CommitAsync();  // _unitOfWork üzerinden de veritabanına kaydını sağlıyorum
            // Cachelemem lazım yani bir data geldi sonuçta
           await CacheAllProductsAsync();
           return entity; // entity mizi Db den id siyle birlikte dönelim

        }

        public async Task<IEnumerable<Product>> AddRangeAsync(IEnumerable<Product> entities)
        {
            await _repository.AddRangeAsync(entities);  
            await _unitOfWork.CommitAsync(); 
            // Cachelemem lazım yani bir data geldi sonuçta
            await CacheAllProductsAsync();
            return entities; 
        }

        public Task<bool> AnyAsync(Expression<Func<Product, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            return Task.FromResult(_memoryCache.Get<IEnumerable<Product>>(CacheProductKey)); // cache deki tüm datayı aldı bizim istediğimiz id li cache i getirdi
        }

        public Task<Product> GetByIdAsync(int id)
        {
            var product = _memoryCache.Get<List<Product>>(CacheProductKey).FirstOrDefault(x => x.Id == id);
            if(product == null)
            {
                throw new NotFoundException($"{typeof(Product).Name}({id}) Not Found");
            }
            return Task.FromResult(product); // awaşt kullanmıyorsam mutlaka Task.FromResult kullanmalıyım
        }

        public Task<CustomResponseDto<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var produts = _memoryCache.Get<IEnumerable<Product>>(CacheProductKey);
            var productsWithCategoryDto = _mapper.Map<List<ProductWithCategoryDto>>(produts);
            return Task.FromResult(CustomResponseDto<List<ProductWithCategoryDto>>.Success(200,productsWithCategoryDto));


        }

        public async Task RemoveAsync(Product entity)
        {
            _repository.Remove(entity);  // _repository üzerinden entity i aldım ve silmek için işaretledim
            await _unitOfWork.CommitAsync(); // Veritabanına bu şekilde kaydettim
            await CacheAllProductsAsync();  // Cache datasından da sildim
        }

        public async Task RemoveRangeAsync(IEnumerable<Product> entities)
        {
            _repository.RemoveRange(entities);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();

        }

        public async Task UpdateAsync(Product entity)
        {
            _repository.Update(entity);
            await _unitOfWork.CommitAsync();
            await CacheAllProductsAsync();
        }

        public IQueryable<Product> Where(Expression<Func<Product, bool>> expression)
        {
            return _memoryCache.Get<List<Product>>(CacheProductKey).Where(expression.Compile()).AsQueryable();  // sorguyu EF Core dan değilde _memoryCache üzerinden yapacağız. expression.Compile() where bir function istediği için yaptım. 
        }

        public async Task CacheAllProductsAsync()  // Tüm Products ları cache lesin. Bu metodu her çağırdığımda sırırdan datayı çekip cache liyor
        {
            await _memoryCache.Set(CacheProductKey, _repository.GetAll().ToListAsync());
        }
    }
}
