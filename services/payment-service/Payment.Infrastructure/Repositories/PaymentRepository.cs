using Microsoft.EntityFrameworkCore;
using Payment.Application.Interfaces;
using Payment.Domain.Entities;
using Payment.Infrastructure.Data;

namespace Payment.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly PaymentDbContext _context;

    public PaymentRepository(PaymentDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(PaymentTransaction transaction)
    {
        await _context.PaymentTransactions.AddAsync(transaction);
    }

    public async Task<PaymentTransaction?> GetByOrderIdAsync(Guid orderId)
    {
        return await _context.PaymentTransactions.FirstOrDefaultAsync(p => p.OrderId == orderId);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}