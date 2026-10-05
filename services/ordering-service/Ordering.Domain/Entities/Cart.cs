using System;
using System.Collections.Generic;
using Ordering.Domain;

namespace Ordering.Domain.Entities;

public class Cart : BaseEntity
{
    public Guid CustomerId { get; set; }

    // Navigation Property
    public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
}