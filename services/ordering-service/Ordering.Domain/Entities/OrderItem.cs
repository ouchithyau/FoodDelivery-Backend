using System;
using Ordering.Domain;

namespace Ordering.Domain.Entities;

public class OrderItem : BaseEntity
{
    public Guid OrderId { get; set; }
    public Guid MenuItemId { get; set; }
    public string ItemName { get; set; } = string.Empty;
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }

    // Navigation Property
    public Order? Order { get; set; }
}