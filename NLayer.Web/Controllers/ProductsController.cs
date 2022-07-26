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
            var categories=_categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);   // Neti mapleyeceksem onnu yazıyorum Map<List<CategoryDto>>(categories) şeklinde
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

            var categories = _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);   // Neyi mapleyeceksem onnu yazıyorum Map<List<CategoryDto>>(categories) şeklinde
            ViewBag.categories = new SelectList(categoriesDto, "Id", "Name");
            return View();  // Eğer başarısız ise category i tekrar yüklesin aynı sayfaya tekrar dönsün
        }
    }
}
