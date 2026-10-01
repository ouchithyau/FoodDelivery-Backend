using Restaurant.Application.DTOs;
using Restaurant.Application.Interfaces;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Services;

public class MenuItemService
{
    private readonly IMenuItemRepository _menuItemRepository;

    public MenuItemService(IMenuItemRepository menuItemRepository)
    {
        _menuItemRepository = menuItemRepository;
    }

    public async Task<IEnumerable<MenuItemDto>> GetMenuItemsByRestaurantIdAsync(Guid restaurantId)
    {
        var items = await _menuItemRepository.GetByRestaurantIdAsync(restaurantId);
        return items.Select(m => new MenuItemDto(
            m.Id, m.Name, m.Description, m.Price, m.ImageUrl,
            m.IsAvailable, m.IsVegetarian, m.RestaurantId, m.CategoryId
        ));
    }

    public async Task<MenuItemDto?> GetMenuItemByIdAsync(Guid id)
    {
        var m = await _menuItemRepository.GetByIdAsync(id);
        if (m == null) return null;

        return new MenuItemDto(
            m.Id, m.Name, m.Description, m.Price, m.ImageUrl,
            m.IsAvailable, m.IsVegetarian, m.RestaurantId, m.CategoryId
        );
    }

    public async Task<Guid> CreateMenuItemAsync(CreateMenuItemDto dto)
    {
        var menuItem = new MenuItem
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            ImageUrl = dto.ImageUrl,
            IsAvailable = true,
            IsVegetarian = dto.IsVegetarian,
            RestaurantId = dto.RestaurantId,
            CategoryId = dto.CategoryId,
            CreatedAt = DateTime.UtcNow
        };

        await _menuItemRepository.AddAsync(menuItem);
        return menuItem.Id;
    }
}