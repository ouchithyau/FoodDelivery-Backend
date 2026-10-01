namespace Restaurant.Application.DTOs;

public record CategoryDto(
    Guid Id,
    string Name,
    Guid RestaurantId
);

public record CreateCategoryDto(
    string Name,
    Guid RestaurantId
);