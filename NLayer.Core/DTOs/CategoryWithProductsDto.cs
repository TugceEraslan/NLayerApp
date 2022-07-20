using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.DTOs
{
    public class CategoryWithProductsDto:CategoryDto   // Hem kategori geldi
    {
        public List<ProductDto> Products { get; set; }  // Hem de kategorilere bağlı products ları List<ProductDto> ile getirdim
                                                        // Artık ICategoryService de bunu dönebilirim
    }
}
