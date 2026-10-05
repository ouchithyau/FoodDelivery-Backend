using MediatR;
using Ordering.Application.Interfaces;

namespace Ordering.Application.Features.Orders.Commands.UpdateOrderStatus;

public class UpdateOrderStatusCommandHandler : IRequestHandler<UpdateOrderStatusCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly string[] _validStatuses = { "Pending", "Accepted", "Preparing", "OutForDelivery", "Delivered" };

    public UpdateOrderStatusCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(UpdateOrderStatusCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        if (order == null) throw new Exception("Order not found.");

        // Business Logic: Status Validation
        if (!_validStatuses.Contains(request.NewStatus))
        {
            throw new Exception($"Invalid status. Allowed statuses are: {string.Join(", ", _validStatuses)}");
        }

        order.OrderStatus = request.NewStatus;
        order.UpdatedAt = DateTime.UtcNow;

        await _orderRepository.UpdateAsync(order);

        return true;
    }
}