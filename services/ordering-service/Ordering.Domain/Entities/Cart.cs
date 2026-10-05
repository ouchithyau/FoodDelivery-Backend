namespace Ordering.Domain.Entities;

public class Cart
{
    public Guid CustomerId { get; set; }
    public List<CartItem> Items { get; set; } = new List<CartItem>();
}

public class CartItem
{
    public Guid MenuItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
}