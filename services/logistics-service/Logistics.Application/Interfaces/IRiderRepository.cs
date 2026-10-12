using Logistics.Domain.Entities;

namespace Logistics.Application.Interfaces;

public interface IRiderRepository
{
    Task<Rider?> GetByIdAsync(Guid id);
    Task<IEnumerable<Rider>> GetAvailableRidersAsync(); 
    Task AddAsync(Rider rider);
    Task UpdateAsync(Rider rider);
    Task SaveChangesAsync();
}