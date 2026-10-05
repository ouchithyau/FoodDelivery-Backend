using System;
using System.Collections.Generic;

namespace Ordering.Application.DTOs;

public record CreateOrderDto(
    Guid CustomerId,
    Guid RestaurantId,
    string DeliveryAddress,
    List<CreateOrderItemDto> Items
);