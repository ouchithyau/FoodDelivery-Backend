using MediatR;
using Ordering.Application.DTOs;
using Ordering.Application.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Queries.GetOrderById;

public class GetOrderByIdQueryHandler : IRequestHandler<GetOrderByIdQuery, OrderDto>
{
    private readonly IOrderRepository _orderRepository;

    public GetOrderByIdQueryHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<OrderDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        if (order == null) throw new Exception("Order not found.");

        return new OrderDto(
            order.Id,
            order.CustomerId,
            order.RestaurantId,
            order.TotalAmount,
            order.OrderStatus,
            order.DeliveryAddress,
            order.CreatedAt,
            order.OrderItems.Select(i => new OrderItemDto(i.MenuItemId, i.ItemName, i.UnitPrice, i.Quantity)).ToList()
        );
    }
}