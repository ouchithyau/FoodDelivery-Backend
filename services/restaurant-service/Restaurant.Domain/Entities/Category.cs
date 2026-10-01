using Restaurant.Domain.Common;

namespace Restaurant.Domain.Entities;

public class Category : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public Guid RestaurantId { get; set; }
    public Restaurant Restaurant { get; set; } = null!;

    public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();
}