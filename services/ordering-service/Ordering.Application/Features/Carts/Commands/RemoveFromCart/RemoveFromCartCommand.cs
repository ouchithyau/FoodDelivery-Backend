using MediatR;
using System;

namespace Ordering.Application.Features.Carts.Commands.RemoveFromCart;

public record RemoveFromCartCommand(Guid CustomerId, Guid MenuItemId) : IRequest<bool>;