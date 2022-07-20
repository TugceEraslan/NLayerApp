using Microsoft.EntityFrameworkCore;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();  // IUnitOfWork ile karþýlaþtýðýmda UnitOfWork ü nesne örneði alacaksýn
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Generic olduðu için typeof olarak ekelyeceðiz.IGenericRepository<> ile karþýlaþtýðýnda GenericRepository<> den nesne örneði al
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICategoryRepository , CategoryRepository>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddDbContext<AppDbContext>(x =>   // AppDbContext için AppDbContext i ekle
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        // API katmanýndaki appsettings.json un ConnectionStrings property sindeki SqlConnection i alacak
        // ikinci parametredeki option => dan ise Migration ý uygulayacaðý  AppDbContext class ýnýn yolunu alacaðým
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    }); 
} );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
