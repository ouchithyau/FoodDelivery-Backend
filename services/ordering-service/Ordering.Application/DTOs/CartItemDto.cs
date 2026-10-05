using System;

namespace Ordering.Application.DTOs;

public record CreateOrderItemDto(
    Guid MenuItemId,
    string ItemName,
    decimal UnitPrice,
    int Quantity
);