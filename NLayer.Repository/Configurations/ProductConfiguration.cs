using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(x => x.Id);  // Id primary key oldu 
            builder.Property(x => x.Id).UseIdentityColumn(); // Id otomatik olarak birer birer artacak
            builder.Property(x => x.Name).IsRequired().HasMaxLength(200); // Name alanı hem zorunlu alan hem de max 200 karakter uzunluğunda
            builder.Property(x => x.Stock).IsRequired(); // Stock alanı da zorunlu olsun
            // $ ################.## decimal(18,2) demek yani toplamda 18 karakter var ama 16 sı virgülden önce 2 si virgülden sonra
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(18,2)");

            builder.ToTable("Products");
            builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryId); // Burdaki builder produts demek
            // Her products ın HasOne(x => x.Category) ile sadece bir category si olabilir. WithMany derken Category ile beraber category e geçtik
            // Her Category nin ise WithMany(x => x.Products) ile birden fazla products ı olabilir.
            // HasForeignKey(x => x.CategoryId); ise (x => x.Products) daki CategoryId demek
        }
    }
}
