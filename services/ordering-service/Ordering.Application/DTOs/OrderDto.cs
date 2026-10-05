using System;
using System.Collections.Generic;

namespace Ordering.Application.DTOs;

public record OrderDto(
    Guid Id,
    Guid CustomerId,
    Guid RestaurantId,
    decimal TotalAmount,
    string OrderStatus,
    string DeliveryAddress,
    DateTime CreatedAt,
    List<OrderItemDto> OrderItems
);

public record OrderItemDto(
    Guid MenuItemId,
    string ItemName,
    decimal UnitPrice,
    int Quantity
);