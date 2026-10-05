using MediatR;
using System;

namespace Ordering.Application.Features.Carts.Commands.AddToCart;

public record AddToCartCommand(
    Guid CustomerId,
    Guid MenuItemId,
    string ItemName,
    decimal UnitPrice,
    int Quantity) : IRequest<bool>;