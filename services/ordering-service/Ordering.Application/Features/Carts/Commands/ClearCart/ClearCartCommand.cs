using MediatR;
using System;

namespace Ordering.Application.Features.Carts.Commands.ClearCart;

public record ClearCartCommand(Guid CustomerId) : IRequest<bool>;