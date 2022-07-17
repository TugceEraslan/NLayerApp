using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NLayer.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Repository.Seeds
{
    internal class CategorySeed : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category { Id=1,Name="Kalemler" }, // Seed Data da identity otomatik artmaz baştan değer vermej gerekir
                new Category { Id=2,Name="Kitaplar" }, 
                new Category { Id=3,Name="Defterler" });
        }
    }
}
