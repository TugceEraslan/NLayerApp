using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayer.API.Filters;
using NLayer.API.Middlewares;
using NLayer.API.Modules;
using NLayer.Core.Repositories;
using NLayer.Core.Services;
using NLayer.Core.UnitOfWorks;
using NLayer.Repository;
using NLayer.Repository.Repositories;
using NLayer.Repository.UnitOfWorks;
using NLayer.Service.Mapping;
using NLayer.Service.Services;
using NLayer.Service.Validations;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttiribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;  // Framework �n kendi belirlemi� oldu�u model filtresini bask�lam�� oldum. ��nk� kendi filtrem var
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMemoryCache();


builder.Services.AddScoped(typeof(NotFoundFilter<>));
//builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();  // IUnitOfWork ile kar��la�t���mda UnitOfWork � nesne �rne�i alacaks�n
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // Generic oldu�u i�in typeof olarak ekelyece�iz.IGenericRepository<> ile kar��la�t���nda GenericRepository<> den nesne �rne�i al
//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
//builder.Services.AddAutoMapper(typeof(MapProfile));

//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IProductService, ProductService>();

//builder.Services.AddScoped<ICategoryRepository , CategoryRepository>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddDbContext<AppDbContext>(x =>   // AppDbContext i�in AppDbContext i ekle
{
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        // API katman�ndaki appsettings.json un ConnectionStrings property sindeki SqlConnection i alacak
        // ikinci parametredeki option => dan ise Migration � uygulayaca��  AppDbContext class �n�n yolunu alaca��m
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    }); 
} );
  builder.Services.AddSwaggerGen(c => {    // .NET 6 i�in, Program.cs'de Swagger Generation'� yap�land�rd�m. Swagger Generation sayfas�nda API tan�m� y�klenemedi hatas� al�yordu
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    c.IgnoreObsoleteActions();
    c.IgnoreObsoleteProperties();
    c.CustomSchemaIds(type => type.FullName);
});

builder.Host.UseServiceProviderFactory
    (new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder => containerBuilder.RegisterModule(new RepoServiceModule()));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
