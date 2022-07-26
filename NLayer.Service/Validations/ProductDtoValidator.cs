using FluentValidation;
using NLayer.Core.DTOs;

namespace NLayer.Service.Validations
{
    public class ProductDtoValidator : AbstractValidator<ProductDto>  // ProductDtoValidator sınıfımın FuluentValidation dan gelen sınıfı implemente etmemiz lazım o sınıfta AbstractValidator. Validator kullanacağım DTO da ProductDto
    {
        public ProductDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName}  is required");

            RuleFor(x => x.Price).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertName} must be greater 0");  //price,double,datetime değer tipli olduklarından dolayı default değerleri var.O yüzden NotNull() ya da NotEmpty() yapmayız.
            RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertName} must be greater 0");
      
        }
    }
}
