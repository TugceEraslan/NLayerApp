using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : CustomBaseController
    {
        private readonly IMapper _mapper;
        private readonly IService<Product> _service;

        public ProductsController(IService<Product> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        // GET   /api/products
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var products=await _service.GetAllAsync();  // Tüm products larımı çekiyorum. products burda entity de geriye dto dönmem lazım
            var productsDtos=_mapper.Map<List<ProductDto>>(products.ToList()); // <List<ProductDto> ile ProductDto larımızı aldım ve yukarıdaki products larımı verdim
                                                                               // productsDtos IEnumarable döndüğü için ben de (products.ToList()) döndüm        
            return CreateActionResult(CustomResponseDto<List<ProductDto>>.Success(200, productsDtos));
        }

        [HttpGet("{id}")]  // www.mysite.com/api/products/5 bu yapıyı [HttpGet("{id}")]  diyerek sağlıyoruz
        // GET   /api/products/5
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);  // Tüm products larımı çekiyorum. products burda entity de geriye dto dönmem lazım
            var productDtos = _mapper.Map<ProductDto>(product); // <List<ProductDto> ile ProductDto larımızı aldım ve yukarıdaki products larımı verdim
                                                                                 // productsDtos IEnumarable döndüğü için ben de (products.ToList()) döndüm        
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(200, productDtos));
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDto)); // productDto yu _mapper.Map<Product> a dönüştür
            var productDtos = _mapper.Map<ProductDto>(product);  // Yukarı satırdan gelen product ı alıp tekrar Dto nesnesine dönüştürüp geri döndürüyorum       
            return CreateActionResult(CustomResponseDto<ProductDto>.Success(201, productDtos));  // Geriye dönerken de 201 durum kodu ile döneyim
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductUpdateDto productDto)
        {
             await _service.UpdateAsync(_mapper.Map<Product>(productDto)); // UpdateAsync de geriye bir şey döndürmeyeceğiz    
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204));  // Geriye bir şey dönmeyeceğim için CustomResponseDto<NoContentDto> boş sınıfımı dönüyorum
        }

        [HttpDelete("{id}")]  // www.mysite.com/api/products/5 bu yapıyı [HttpDelete("{id}")]  diyerek sağlıyoruz
        // DELETE   api/products/5
        public async Task<IActionResult> Remove(int id)
        {
             var product = await _service.GetByIdAsync(id);

            //if(product== null)
            //{
            //    return CreateActionResult(CustomResponseDto<NoContentDto>.Fail(404,"Bu id ye sahip ürün bulunamadı"));
            //}
             await _service.RemoveAsync(product);  // 
                
            return CreateActionResult(CustomResponseDto<NoContentDto>.Success(204)); // Sadece başarılı olduğu durumu döneceğim. Geriye boş sınıf dönmek için de CustomResponseDto<NoContentDto> ı döneceğim
        }

    }
}
