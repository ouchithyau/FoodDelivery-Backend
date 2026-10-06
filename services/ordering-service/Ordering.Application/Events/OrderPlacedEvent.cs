namespace Ordering.Application.Events;

public record OrderPlacedEvent(Guid OrderId, Guid CustomerId, decimal TotalPrice);