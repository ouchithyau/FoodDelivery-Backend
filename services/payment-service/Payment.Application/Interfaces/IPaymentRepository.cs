using System.Threading.Tasks;
using Payment.Domain.Entities;

namespace Payment.Application.Interfaces;

public interface IPaymentRepository
{
    Task AddAsync(PaymentTransaction transaction);
    Task<PaymentTransaction?> GetByOrderIdAsync(Guid orderId);
    Task SaveChangesAsync();
}