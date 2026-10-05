using Ordering.Domain.Entities;

namespace Ordering.Application.Interfaces;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    Task<IEnumerable<Order>> GetByCustomerIdAsync(Guid customerId);
    Task<Order> AddAsync(Order order);
    Task UpdateAsync(Order order);
}