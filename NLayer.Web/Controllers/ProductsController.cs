using Microsoft.AspNetCore.Mvc;
using NLayer.Core.Services;

namespace NLayer.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _services;

        public ProductsController(IProductService services)   // IProductService in ctor unu ekledim
        {
            _services = services;
        }

        public async Task<IActionResult> Index()  // Listeleme sayfam olsun
        {
            return View(await _services.GetProductsWithCategory());
        }
    }
}
