using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services
{
    public class ProductService : Service<Product>, IProductService   // Service katmanından gelen bir çok hazır metodu alsın ve IProductService i de implemente etsin
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepository productRepository) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<List<ProductWithCategoryDto>> GetProductsWithCategory()
        {

            var products = await _productRepository.GetProductsWithCategory();    // Tüm datayı alayım.
            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);  // Success() metodu List<ProductWithCategoryDto>>(products) beklediği için _mapper.Map<List<ProductWithCategoryDto>>(products); şeklinde yaptık
            return productsDto;
        }
    }
}
