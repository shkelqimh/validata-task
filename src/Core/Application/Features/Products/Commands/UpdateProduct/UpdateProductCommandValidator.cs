using Domain.Constants;
using FluentValidation;

namespace Application.Features.Products.Commands.UpdateProduct;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(product => product.Id)
            .NotNull()
            .NotEmpty();
        
        RuleFor(product => product.Name)
            .NotNull()
            .NotEmpty()
            .MinimumLength(Constants.Product.NameMinLength)
            .MaximumLength(Constants.Product.NameMaxLength);

        RuleFor(product => product.Price)
            .NotNull()
            .GreaterThan(0);
    }
}