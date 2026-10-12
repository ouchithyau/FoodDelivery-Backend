using Logistics.Domain.Entities;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Logistics.Infrastructure.Data;

public class LogisticsDbContext : DbContext
{
    public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options) : base(options) { }

    public DbSet<Delivery> Deliveries { get; set; }
    public DbSet<Rider> Riders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        
        modelBuilder.Entity<Rider>().HasKey(r => r.Id);

        modelBuilder.Entity<Delivery>().HasKey(d => d.Id);
    }
}