using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;

namespace NLayer.Repository.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id); // HasKey diyerek Id yi primamary key olarak belirledim
            builder.Property(x => x.Id).UseIdentityColumn(); // UseIdentityColumn() içi boş kalırsa default olarak Idendity değeri birer birer artar
            builder.Property(x => x.Name).IsRequired().HasMaxLength(50); // Category tablomdaki Name alanım zorunlu olsun IsRequired() ile max alacağı karakter sayısı ise 50 olsun

            builder.ToTable("Categories");  // Veritabanında ki tablomun ismini açık açık vermem ya da değiştirmem gerekirse 

        }
    }
}
