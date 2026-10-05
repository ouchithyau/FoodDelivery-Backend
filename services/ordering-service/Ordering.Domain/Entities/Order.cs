namespace Ordering.Domain.Entities;

public class Order : BaseEntity
{
    public Guid CustomerId { get; set; }
    public Guid RestaurantId { get; set; }
    public decimal TotalAmount { get; set; }
    public string OrderStatus { get; set; } = "Pending"; // Pending, Preparing, OutForDelivery, Delivered, Cancelled
    public string DeliveryAddress { get; set; } = string.Empty;

    // Navigation Property
    public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}