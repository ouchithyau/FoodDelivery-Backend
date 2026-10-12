using Microsoft.EntityFrameworkCore;
using Support.Application.Interfaces;
using Support.Domain.Entities;
using Support.Infrastructure.Data;

namespace Support.Infrastructure.Repositories
{
    public class SupportRepository : ISupportRepository
    {
        private readonly SupportDbContext _context;

        public SupportRepository(SupportDbContext context)
        {
            _context = context;
        }

        public async Task AddDisputeTicketAsync(DisputeTicket ticket)
        {
            await _context.DisputeTickets.AddAsync(ticket);
        }

        public async Task<IEnumerable<DisputeTicket>> GetTicketsByCustomerIdAsync(Guid customerId)
        {
            return await _context.DisputeTickets
                .Where(t => t.CustomerId == customerId)
                .ToListAsync();
        }

        public async Task AddReviewAsync(Review review)
        {
            await _context.Reviews.AddAsync(review);
        }

        public async Task AddRatingAsync(Rating rating)
        {
            await _context.Ratings.AddAsync(rating);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}