using Support.Domain.Enums;

namespace Support.Domain.Entities
{
    public class DisputeTicket
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string? Description { get; set; }
        public TicketStatus Status { get; set; } = TicketStatus.Open;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ResolvedAt { get; set; }
    }
}