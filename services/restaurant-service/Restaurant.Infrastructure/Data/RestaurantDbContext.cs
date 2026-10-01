using Microsoft.EntityFrameworkCore;
using Restaurant.Domain.Entities;
using System.Reflection.Emit;

namespace Restaurant.Infrastructure.Data;

public class RestaurantDbContext : DbContext
{
    public RestaurantDbContext(DbContextOptions<RestaurantDbContext> options) : base(options)
    {
    }

    public DbSet<Domain.Entities.Restaurant> Restaurants => Set<Domain.Entities.Restaurant>();
    public DbSet<MenuItem> MenuItems => Set<MenuItem>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Domain.Entities.Restaurant>(entity =>
        {
            entity.HasKey(r => r.Id);
            entity.Property(r => r.Name).IsRequired().HasMaxLength(150);
            entity.HasMany(r => r.MenuItems)
                  .WithOne(m => m.Restaurant)
                  .HasForeignKey(m => m.RestaurantId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MenuItem>(entity =>
        {
            entity.HasKey(m => m.Id);
            entity.Property(m => m.Name).IsRequired().HasMaxLength(100);
            entity.Property(m => m.Price).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Name).IsRequired().HasMaxLength(100);
        });
    }
}