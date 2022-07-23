using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using NLayer.Core.Services;

namespace NLayer.API.Filters
{
    
    public class NotFoundFilter<T> : IAsyncActionFilter where T : BaseEntity
    {
        private readonly IService<T> _service;

        public NotFoundFilter(IService<T> service)  // Filter ctor ımda  service class ı kullandıysam API Program.cs e de bunu eklemem lazım
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next) // Buradaki next in amacı hiçbir filter a takılmazsa next üzerinden devam edecek 
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault(); // FirstOrDefault ile ilk gellen id yi al. Zaten bir tane

            if(idValue == null)
            {
                await next.Invoke();  //id null geldiyse yoluna devam et
                return;
            }
            var id=(int)idValue;  // Eğer id si varsa bu id yi idValue ya cast ediyorum
            var anyEntity = await _service.AnyAsync(x => x.Id==id);  // Arkasında entity var mı yok mu bunu kontrol ediyorum. Kontol edebilmem için bir servis katmanına ihtiyacım var. O yüzden IService si çağırıyoruz. ctor ile _service e gidip .GetByIdAsync(id) yi çekiyoruz

            if(anyEntity) // Eğer entity varsa
            {
                await next.Invoke();
                return;
            }

            // Buraya kadar geldiyse böyle bir id ye sahip bir data yok demektir
            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404, $"{typeof(T).Name}({id}) Not Found"));

        }
    }
}
