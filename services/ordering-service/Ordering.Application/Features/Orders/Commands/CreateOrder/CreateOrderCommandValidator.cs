using FluentValidation;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
    public CreateOrderCommandValidator()
    {
        RuleFor(x => x.CustomerId).NotEmpty().WithMessage("Customer is required.");
        RuleFor(x => x.RestaurantId).NotEmpty().WithMessage("Restaurant is required.");
        RuleFor(x => x.DeliveryAddress).NotEmpty().WithMessage("Delivery address is required.");
        RuleFor(x => x.Items).NotEmpty().WithMessage("Order must contain at least one item.");
    }
}