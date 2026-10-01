namespace Auth.Domain.Entities;

public class Address
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Street { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string ZipCode { get; set; } = string.Empty;

    // Foreign Key
    public Guid UserId { get; set; }
    public User User { get; set; } = null!;
}