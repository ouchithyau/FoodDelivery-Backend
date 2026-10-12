using Microsoft.EntityFrameworkCore;
using Support.Domain.Entities;

namespace Support.Infrastructure.Data
{
    public class SupportDbContext : DbContext
    {
        public SupportDbContext(DbContextOptions<SupportDbContext> options) : base(options) { }

        public DbSet<DisputeTicket> DisputeTickets { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Rating> Ratings { get; set; }
    }
}