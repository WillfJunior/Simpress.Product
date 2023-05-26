using FluentValidation;

namespace Simpress.Product.Domain.Entities.Models.Validations
{
    public class ProductValidation : AbstractValidator<Product>
    {
        public ProductValidation()
        {
            RuleFor(p => p.Description)
                .NotEmpty()
                .WithMessage("The field {PropertyName} is required")
                .Length(5, 50)
                .WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");

            RuleFor(p => p.Price).GreaterThan(0).WithMessage("The field {PropertyName} must be greater than 0");
        }
    }
}
