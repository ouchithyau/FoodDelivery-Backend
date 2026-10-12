namespace Logistics.Application.Events;

public class PaymentCompletedEvent
{
    public Guid OrderId { get; set; }
    public string PickupAddress { get; set; } = string.Empty;
    public string DropoffAddress { get; set; } = string.Empty;
}