using Microsoft.EntityFrameworkCore;
using Restaurant.Application.Interfaces;
using Restaurant.Infrastructure.Data;
namespace Restaurant.Infrastructure.Repositories;

public class RestaurantRepository : IRestaurantRepository
{
    private readonly RestaurantDbContext _context;

    public RestaurantRepository(RestaurantDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Domain.Entities.Restaurant>> GetAllAsync()
    {
        return await _context.Restaurants.ToListAsync();
    }

    public async Task<Domain.Entities.Restaurant?> GetByIdAsync(Guid id)
    {
        return await _context.Restaurants
            .Include(r => r.MenuItems)
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task AddAsync(Domain.Entities.Restaurant restaurant)
    {
        await _context.Restaurants.AddAsync(restaurant);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Domain.Entities.Restaurant restaurant)
    {
        _context.Restaurants.Update(restaurant);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var restaurant = await _context.Restaurants.FindAsync(id);
        if (restaurant != null)
        {
            _context.Restaurants.Remove(restaurant);
            await _context.SaveChangesAsync();
        }
    }
}