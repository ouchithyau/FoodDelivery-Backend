using Support.Domain.Entities;

namespace Support.Application.Interfaces
{
    public interface ISupportRepository
    {
        Task AddDisputeTicketAsync(DisputeTicket ticket);
        Task<IEnumerable<DisputeTicket>> GetTicketsByCustomerIdAsync(Guid customerId);
        Task AddReviewAsync(Review review);
        Task AddRatingAsync(Rating rating);
        Task SaveChangesAsync();
    }
}