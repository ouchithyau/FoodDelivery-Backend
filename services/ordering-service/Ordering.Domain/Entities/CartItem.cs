using System;
using Ordering.Domain;

namespace Ordering.Domain.Entities;

public class CartItem : BaseEntity
{
    public Guid CartId { get; set; }
    public Guid MenuItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }

    // Navigation Property
    public Cart? Cart { get; set; }
}