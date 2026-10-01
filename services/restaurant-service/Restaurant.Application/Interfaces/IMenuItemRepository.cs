using Restaurant.Domain.Entities;

namespace Restaurant.Application.Interfaces;

public interface IMenuItemRepository
{
    Task<IEnumerable<MenuItem>> GetByRestaurantIdAsync(Guid restaurantId);
    Task<MenuItem?> GetByIdAsync(Guid id);
    Task AddAsync(MenuItem menuItem);
    Task UpdateAsync(MenuItem menuItem);
    Task DeleteAsync(Guid id);
}