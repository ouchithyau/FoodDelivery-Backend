namespace Restaurant.Application.DTOs;

public record MenuItemDto(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    string ImageUrl,
    bool IsAvailable,
    bool IsVegetarian,
    Guid RestaurantId,
    Guid CategoryId
);

public record CreateMenuItemDto(
    string Name,
    string Description,
    decimal Price,
    string ImageUrl,
    bool IsVegetarian,
    Guid RestaurantId,
    Guid CategoryId
);