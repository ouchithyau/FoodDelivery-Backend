namespace Restaurant.Application.DTOs;

public record RestaurantDto(
    Guid Id,
    string Name,
    string Description,
    string Address,
    string PhoneNumber,
    string LogoUrl,
    bool IsOpen,
    decimal DeliveryFee,
    int EstimatedDeliveryTimeMinutes
);

public record CreateRestaurantDto(
    string Name,
    string Description,
    string Address,
    string PhoneNumber,
    string LogoUrl,
    decimal DeliveryFee,
    int EstimatedDeliveryTimeMinutes
);