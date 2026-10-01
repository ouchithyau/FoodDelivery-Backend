using Restaurant.Domain.Common;

namespace Restaurant.Domain.Entities;

public class Restaurant : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string LogoUrl { get; set; } = string.Empty;
    public bool IsOpen { get; set; } = true; // Real-world status: Open/Closed for delivery
    public decimal DeliveryFee { get; set; }
    public int EstimatedDeliveryTimeMinutes { get; set; } = 30; // e.g., 30 mins average

    // Navigation Properties
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}