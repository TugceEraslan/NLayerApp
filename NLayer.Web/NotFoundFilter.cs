using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.Web
{
    public class NotFoundFilter<T>:IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;  // Gelen <T> için bir de service katmanına ihtiyacım var

        public NotFoundFilter(IService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault(); // FirstOrDefault ile ilk gellen id yi al. Zaten bir tane

            if (idValue == null)
            {
                await next.Invoke();  //id null geldiyse yoluna devam et
                return;
            }
            var id = (int)idValue;  // Eğer id si varsa bu id yi idValue ya cast ediyorum
            var anyEntity = await _service.AnyAsync(x => x.Id == id);  // Arkasında entity var mı yok mu bunu kontrol ediyorum. Kontol edebilmem için bir servis katmanına ihtiyacım var. O yüzden IService si çağırıyoruz. ctor ile _service e gidip .GetByIdAsync(id) yi çekiyoruz

            if (anyEntity) // Eğer entity varsa
            {
                await next.Invoke();
                return;
            }

            var errorViewModel = new ErrorViewModel();  // Error Dto sunu oluşturdum
            errorViewModel.Errors.Add($"{typeof(T).Name}({id} Not Found!!!)");  // Hatamızı ekledik

            context.Result = new RedirectToActionResult("Error", "Home", errorViewModel);  // Error sayfasına yani HomeController içerindeki Error action ına ve model olarakta errorViewModel i verdim

        }
    }
}
