namespace NLayer.Core.DTOs
{
    public class CategoryWithProductsDto : CategoryDto   // Hem kategori geldi
    {
        public List<ProductDto> Products { get; set; }  // Hem de kategorilere bağlı products ları List<ProductDto> ile getirdim
                                                        // Artık ICategoryService de bunu dönebilirim
    }
}
