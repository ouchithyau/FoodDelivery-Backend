using Logistics.Domain.Entities;

namespace Logistics.Application.Interfaces;

public interface IDeliveryRepository
{
    Task<Delivery?> GetByIdAsync(Guid id);
    Task<Delivery?> GetByOrderIdAsync(Guid orderId);
    Task AddAsync(Delivery delivery);
    Task UpdateAsync(Delivery delivery);
    Task SaveChangesAsync();
}