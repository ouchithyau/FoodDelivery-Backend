using MediatR;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrderStatus;

public record UpdateOrderStatusCommand(Guid OrderId, string NewStatus) : IRequest<bool>;