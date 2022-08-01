using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _services;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService services, ICategoryService categoryService, IMapper mapper)   // IProductService in ctor unu ekledim
        {
            _services = services;
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()  // Listeleme sayfam olsun
        {
            return View(await _services.GetProductsWithCategory());
        }

        [HttpGet]
        public async Task<IActionResult> Save()
        {
            // Product ve Category ler arasında bire çok bir ilişki var
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());   // Neti mapleyeceksem onnu yazıyorum Map<List<CategoryDto>>(categories) şeklinde
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Save(ProductDto productDto)
        {

            if (ModelState.IsValid)
            {
                await _services.AddAsync(_mapper.Map<Product>(productDto));  // productDto yu Product a dönüştür
                return RedirectToAction(nameof(Index));  // Kaydettikten sonra direkt index sayfasına gitsin. Tip güvenli bir şekilde gitmesi için nameof ekledim başına
            }

            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());   // Neyi mapleyeceksem onnu yazıyorum Map<List<CategoryDto>>(categories) şeklinde
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();  // Eğer başarısız ise category i tekrar yüklesin aynı sayfaya tekrar dönsün amam category i aynı sayfada tutarak dönsün
        }
        [ServiceFilter(typeof(NotFoundFilter<Product>))]
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product=await _services.GetByIdAsync(id);
            // Drop DownList i doldurmam lazım
            // Product ve Category ler arasında bire çok bir ilişki var
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());   // Neyi mapleyeceksem onu yazıyorum Map<List<CategoryDto>>(categories) şeklinde
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name",product.CategoryId);

            return View(_mapper.Map<ProductDto>(product));

        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductDto productDto)
        {
            if (ModelState.IsValid)  // IsValid değilse herhangi bir alanı boş bıraktıysa ikinci blokta tekrar çekmesi için  ???
            {
                await _services.UpdateAsync(_mapper.Map<Product>(productDto));
                return RedirectToAction(nameof(Index)); // Başarılıysa Indexe gitsin
            }
            //
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories.ToList());   // Neyi mapleyeceksem onu yazıyorum Map<List<CategoryDto>>(categories) şeklinde
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name", productDto.CategoryId);
            return View (productDto);  // productDto nesnemi döndüm. Başarısızsa tekrar dolsun

        }
        
        public async Task<IActionResult> Remove(int id)
        {
            var product= await _services.GetByIdAsync(id);  // Bana önce product ı ver
            await _services.RemoveAsync(product);
            return RedirectToAction(nameof(Index));
        }

    }
}
