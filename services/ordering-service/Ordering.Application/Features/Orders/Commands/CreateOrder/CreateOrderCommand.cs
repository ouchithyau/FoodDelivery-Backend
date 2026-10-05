using MediatR;
using Ordering.Application.DTOs; 

namespace Ordering.Application.Features.Orders.Commands.CreateOrder;

public record CreateOrderCommand(
    Guid CustomerId,
    Guid RestaurantId,
    string DeliveryAddress,
    List<CreateOrderItemDto> Items) : IRequest<Guid>;