using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class ProductWithCategoryDto: ProductDto  // ProductDto dan miras aldım ki ProductDto dan öncelikle entity lerim gelsin
    {
        public CategoryDto Category { get; set; }  // CategoryDto yu aldım. Bu endpoint için döneceğim Dto nesnem hazır.

        // Repository ler geriye entity dönerken , Service ler dirakt olarak API ın isteyeceği Dto yu otomatik bir şekilde dönüyor
    }
}
