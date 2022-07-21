using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Filters
{
    public class ValidateFilterAttiribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)  // Hata oluşursa ilgili hatalar context in ModelState ine yükleniyor
            {
                var errors=context.ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();  // SelectMany ile tek tek alıyorum. x artık benim için ModelState enty oluyor. Select(x => x.ErrorMessage).ToList() ile hata mesajlarını gösteriyorum
                context.Result=new  BadRequestObjectResult(CustomResponseDto<NoContentDto>.Fail(400,errors));  // BadRequestObjectResult response ın body sinde hatayı görmek istersek kullanırız

            }
        }
    }
}
