using MediatR;
using Ordering.Application.Interfaces;
using Ordering.Domain.Entities;

namespace Ordering.Application.Features.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Guid>
{
    private readonly IOrderRepository _orderRepository;

    public CreateOrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Guid> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        // Business Logic: Total Amount Calculation
        decimal calculatedTotal = request.Items.Sum(item => item.UnitPrice * item.Quantity);

        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            RestaurantId = request.RestaurantId,
            DeliveryAddress = request.DeliveryAddress,
            TotalAmount = calculatedTotal,
            OrderStatus = "Pending", // Initial State
            CreatedAt = DateTime.UtcNow,
            OrderItems = request.Items.Select(i => new OrderItem
            {
                Id = Guid.NewGuid(),
                MenuItemId = i.MenuItemId,
                ItemName = i.ItemName,
                UnitPrice = i.UnitPrice,
                Quantity = i.Quantity
            }).ToList()
        };

        var createdOrder = await _orderRepository.AddAsync(order);

        

        return createdOrder.Id;
    }
}