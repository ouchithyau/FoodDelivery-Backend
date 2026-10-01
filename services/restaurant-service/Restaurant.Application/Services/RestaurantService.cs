using Restaurant.Application.DTOs;
using Restaurant.Application.Interfaces;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Services;

public class RestaurantService
{
    private readonly IRestaurantRepository _restaurantRepository;

    public RestaurantService(IRestaurantRepository restaurantRepository)
    {
        _restaurantRepository = restaurantRepository;
    }

    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurantsAsync()
    {
        var restaurants = await _restaurantRepository.GetAllAsync();
        return restaurants.Select(r => new RestaurantDto(
            r.Id, r.Name, r.Description, r.Address, r.PhoneNumber,
            r.LogoUrl, r.IsOpen, r.DeliveryFee, r.EstimatedDeliveryTimeMinutes
        ));
    }

    public async Task<RestaurantDto?> GetRestaurantByIdAsync(Guid id)
    {
        var r = await _restaurantRepository.GetByIdAsync(id);
        if (r == null) return null;

        return new RestaurantDto(
            r.Id, r.Name, r.Description, r.Address, r.PhoneNumber,
            r.LogoUrl, r.IsOpen, r.DeliveryFee, r.EstimatedDeliveryTimeMinutes
        );
    }

    public async Task<Guid> CreateRestaurantAsync(CreateRestaurantDto dto)
    {
        var restaurant = new Restaurant.Domain.Entities.Restaurant
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Address = dto.Address,
            PhoneNumber = dto.PhoneNumber,
            LogoUrl = dto.LogoUrl,
            DeliveryFee = dto.DeliveryFee,
            EstimatedDeliveryTimeMinutes = dto.EstimatedDeliveryTimeMinutes,
            IsOpen = true,
            CreatedAt = DateTime.UtcNow
        };

        await _restaurantRepository.AddAsync(restaurant);
        return restaurant.Id;
    }

    // Toggle Restaurant Open/Closed status for Food Delivery
    public async Task ToggleRestaurantStatusAsync(Guid id)
    {
        var restaurant = await _restaurantRepository.GetByIdAsync(id);
        if (restaurant != null)
        {
            restaurant.IsOpen = !restaurant.IsOpen;
            restaurant.UpdatedAt = DateTime.UtcNow;
            await _restaurantRepository.UpdateAsync(restaurant);
        }
    }
}