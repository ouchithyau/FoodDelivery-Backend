namespace Support.Application.DTOs
{
    public class CreateTicketDto
    {
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}