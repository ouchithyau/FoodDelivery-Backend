namespace Logistics.Domain.Entities;

public class Delivery
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; } 
    public Guid? RiderId { get; private set; } 

    public string PickupAddress { get; set; } = string.Empty;
    public string DropoffAddress { get; set; } = string.Empty;

    public string Status { get; private set; } = "Pending"; 
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeliveredAt { get; private set; }

    public Delivery(Guid id, Guid orderId, string pickupAddress, string dropoffAddress)
    {
        Id = id;
        OrderId = orderId;
        PickupAddress = pickupAddress;
        DropoffAddress = dropoffAddress;
    }

    
    public void AssignRider(Guid riderId)
    {
        RiderId = riderId;
        Status = "Assigned";
    }

   
    public void MarkAsPickedUp()
    {
        Status = "PickedUp";
    }

    
    public void MarkAsDelivered()
    {
        Status = "Delivered";
        DeliveredAt = DateTime.UtcNow;
    }
}