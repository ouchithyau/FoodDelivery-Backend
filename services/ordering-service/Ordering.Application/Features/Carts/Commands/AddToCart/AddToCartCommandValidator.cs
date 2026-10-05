using FluentValidation;

namespace Ordering.Application.Features.Carts.Commands.AddToCart;

public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
{
    public AddToCartCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer ID is required.");
        RuleFor(x => x.MenuItemId).NotEmpty().WithMessage("Menu Item ID is required.");
        RuleFor(x => x.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than zero.");
        RuleFor(x => x.UnitPrice).GreaterThan(0).WithMessage("Price must be valid.");
    }
}