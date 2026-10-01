using Restaurant.Domain.Common;

namespace Restaurant.Domain.Entities;

public class MenuItem : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsAvailable { get; set; } = true; // In-stock or out of stock status
    public bool IsVegetarian { get; set; } = false;

    // Foreign Keys
    public Guid RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;

    public Guid CategoryId { get; set; }
    public Category Category { get; set; } = null!;
}