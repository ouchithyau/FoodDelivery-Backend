using System;
using System.Collections.Generic;

namespace Ordering.Application.DTOs;

public record CartDto(
    Guid CustomerId,
    List<CartItemDto> Items
);

public record CartItemDto(
    Guid MenuItemId,
    string ItemName,
    decimal UnitPrice,
    int Quantity
);