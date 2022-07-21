using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NLayer.API.Filters;
using NLayer.Core.DTOs;

namespace NLayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    [ValidateFilterAttiribute]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]  // Endpoint olmadığını belirtmek için [NonAction] diyorum
        public IActionResult CreateActionResult<T>(CustomResponseDto<T> response)
        {
            if (response.StatusCode == 204)  // 204 durum kodu NoContent anlamına geliyor
                return new ObjectResult(null)  // Döneceği data null olsun
                {
                    StatusCode = response.StatusCode // İçeriğimde dönecek durum kodu response.StatusCode dan dönen kod olsun
                };

            return new ObjectResult(response) // Dönen durum kodu 204 ten farklı ise
            {
                StatusCode = response.StatusCode  
            };
        }
    }
}
