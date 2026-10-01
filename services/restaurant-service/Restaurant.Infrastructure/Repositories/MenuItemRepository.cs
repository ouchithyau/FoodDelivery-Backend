using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Interfaces;
using Restaurant.Domain.Entities;
using Restaurant.Infrastructure.Data;

namespace Restaurant.Infrastructure.Repositories;

public class MenuItemRepository : IMenuItemRepository
{
    private readonly RestaurantDbContext _context;

    public MenuItemRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MenuItem>> GetByRestaurantIdAsync(Guid restaurantId)
    {
        return await _context.MenuItems
            .Where(m => m.RestaurantId == restaurantId)
            .ToListAsync();
    }

    public async Task<MenuItem?> GetByIdAsync(Guid id)
    {
        return await _context.MenuItems
            .Include(m => m.Category)
            .Include(m => m.Restaurant)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddAsync(MenuItem menuItem)
    {
        await _context.MenuItems.AddAsync(menuItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MenuItem menuItem)
    {
        _context.MenuItems.Update(menuItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var item = await _context.MenuItems.FindAsync(id);
        if (item != null)
        {
            _context.MenuItems.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}