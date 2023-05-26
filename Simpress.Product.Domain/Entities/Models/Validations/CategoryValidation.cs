using FluentValidation;

namespace Simpress.Product.Domain.Entities.Models.Validations
{
    public  class CategoryValidation : AbstractValidator<Category>
    {
        public CategoryValidation()
        {
            RuleFor(c => c.Description)
                .NotEmpty()
                .WithMessage("The field {PropertyName} is required")
                .Length(5, 50)
                .WithMessage("The field {PropertyName} must be between {MinLength} and {MaxLength} characters");
        }
    }
}
