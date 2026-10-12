using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using Logistics.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure.Repositories;

public class RiderRepository : IRiderRepository
{
    private readonly LogisticsDbContext _context;

    public RiderRepository(LogisticsDbContext context)
    {
        _context = context;
    }

    public async Task<Rider?> GetByIdAsync(Guid id) => await _context.Riders.FindAsync(id);

    public async Task<IEnumerable<Rider>> GetAvailableRidersAsync()
        => await _context.Riders.Where(r => r.IsAvailable).ToListAsync();

    public async Task AddAsync(Rider rider) => await _context.Riders.AddAsync(rider);

    public async Task UpdateAsync(Rider rider) => await Task.Run(() => _context.Riders.Update(rider));

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}