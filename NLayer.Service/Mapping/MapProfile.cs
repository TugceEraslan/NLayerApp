using AutoMapper;
using NLayer.Core.DTOs;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Service.Mapping
{
    public class MapProfile : Profile  // MapProfile class ım Profile sınıfından miras alacak
    {
        public MapProfile()
        {// Neyi neye map lemek istiyorsak onu belirtmemiz gerek
            {
                CreateMap<Product, ProductDto>().ReverseMap();  // Product ı ProductDto sınıfına dönüştürebilirim
                                                                // ReverseMap() dersek Product ı ProductDto sınıfına ya da ProductDto sınıfını Product a çevirebiliriz

                CreateMap<Category, CategoryDto>().ReverseMap();
                CreateMap<ProductFeature, ProductFeatureDto>().ReverseMap();
                CreateMap<ProductUpdateDto, Product>(); // Güncelleme işlemi için ProductUpdateDto gelirse Product a dönüştür yani sadece entity e dönüştürmek için.
                                                        //Tersini yapmaya gerek yok o yüzden sonuna .ReverseMap(); eklemedim
                CreateMap<Product, ProductWithCategoryDto>();  // Product ı, ProductWithCategoryDto ya dönüştürüyorum
            }
        }
    }
}
