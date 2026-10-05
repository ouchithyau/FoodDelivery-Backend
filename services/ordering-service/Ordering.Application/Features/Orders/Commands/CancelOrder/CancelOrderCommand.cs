using MediatR;
using System;

namespace Ordering.Application.Features.Orders.Commands.CancelOrder;

public record CancelOrderCommand(Guid OrderId) : IRequest<bool>;