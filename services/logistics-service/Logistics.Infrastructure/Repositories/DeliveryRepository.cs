using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;
using Logistics.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Logistics.Infrastructure.Repositories;

public class DeliveryRepository : IDeliveryRepository
{
    private readonly LogisticsDbContext _context;

    public DeliveryRepository(LogisticsDbContext context)
    {
        _context = context;
    }

    public async Task<Delivery?> GetByIdAsync(Guid id) => await _context.Deliveries.FindAsync(id);

    public async Task<Delivery?> GetByOrderIdAsync(Guid orderId) => await _context.Deliveries.FirstOrDefaultAsync(d => d.OrderId == orderId);

    public async Task AddAsync(Delivery delivery) => await _context.Deliveries.AddAsync(delivery);

    public async Task UpdateAsync(Delivery delivery) => await Task.Run(() => _context.Deliveries.Update(delivery));

    public async Task SaveChangesAsync() => await _context.SaveChangesAsync();
}