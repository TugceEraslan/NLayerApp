using Microsoft.EntityFrameworkCore;
using NLayer.Core.Models;
using System.Reflection;

namespace NLayer.Repository
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) // DbContextOptions ile beraber veritabanı yolunu startup dosyasından vereceğim.
        {

        }

        // Her bir entity me karşı bir DbSet oluşturacağım
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeature> ProductFeatures { get; set; }
        public override int SaveChanges()
        {
            foreach (var item in ChangeTracker.Entries())  // Entity lerimizi izliyoruz foreach le dönüp
            {
                if (item.Entity is BaseEntity entityReference)  // item.Entity den geleni referans olarak aldım
                {
                    switch (item.Entity)
                    {
                        case EntityState.Added:   // Entity eklenme durumu varsa
                            {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                            }
                        case EntityState.Modified:  // Güncelleme ise
                            {
                                Entry(entityReference).Property(x => x.CreatedDate).IsModified = false;  // EntityState.Modified olduğunda Property(x => x.CreatedDate).IsModified=false; demek CreatedDate e dokunma bu alan Db deki ile aynı kalsın. O yüzden .IsModified=false oldu
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                            }
                    }
                }

            }
            return base.SaveChanges();
        }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var item in ChangeTracker.Entries())  // Entity lerimizi izliyoruz foreach le dönüp
            {
                if(item.Entity is BaseEntity entityReference)  // item.Entity den geleni referans olarak aldım
                {
                    switch (item.Entity)
                    {
                        case EntityState.Added:   // Entity eklenme durumu varsa
                        {
                                entityReference.CreatedDate = DateTime.Now;
                                break;
                        }      
                        case EntityState.Modified:  // Güncelleme ise
                        {
                                entityReference.UpdatedDate = DateTime.Now;
                                break;
                        }
                    }
                }

            }

            return base.SaveChangesAsync(cancellationToken);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // Yani çalışmış olduğum Assembly leri(Repository katmanında  Configurations dosyasındaki Confifuration ile biten class ları  ) tara diyorum
            // Bunu da IEntityTypeConfiguration interface ine sahip tüm class ları bularak yapıyor

            modelBuilder.Entity<ProductFeature>().HasData(new ProductFeature()
            {
                Id = 1,
                Color = "Kırmızı",
                Height = 100,
                Width = 500,
                ProductId = 1
            },
            new ProductFeature()
            {
                Id = 2,
                Color = "Mavi",
                Height = 300,
                Width = 500,
                ProductId = 2
            }
            );
            base.OnModelCreating(modelBuilder);
        }

    }
}
