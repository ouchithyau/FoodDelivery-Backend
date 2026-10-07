using MassTransit;
using Microsoft.EntityFrameworkCore;
using Payment.Domain.Entities;

namespace Payment.Infrastructure.Data;

public class PaymentDbContext : DbContext
{
    public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options) { }

    public DbSet<PaymentTransaction> PaymentTransactions { get; set; }
}