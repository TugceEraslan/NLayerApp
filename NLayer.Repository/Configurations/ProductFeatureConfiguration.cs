using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Configurations
{
    internal class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasKey(x => x.Id); // Id primary key olacak
            builder.Property(x => x.Id).UseIdentityColumn(); // Id otomatik olarak birer birer artacak
            builder.HasOne(x => x.Product).WithOne(x => x.ProductFeature).HasForeignKey<ProductFeature>(x => x.ProductId); // Burada ki builder ProductFeature olduğu için
            // HasOne(x => x.Product) ile ProductFeature bir tane Product a bağlı olabilir
            // Artık product tayım onun da ProductFeature ile birerbir ilişkisi olduğu için WithOne(x => x.ProductFeature) yazdım
            // Foreign Key im ProductFeature entity m içinde olduğu için HasForeignKey<ProductFeature>(x => x.ProductId) şeklinde yazdım
        }
    }
}
