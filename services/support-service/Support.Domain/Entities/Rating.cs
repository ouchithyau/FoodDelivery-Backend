namespace Support.Domain.Entities
{
    public class Rating
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public int Score { get; set; } // 1 to 5
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}