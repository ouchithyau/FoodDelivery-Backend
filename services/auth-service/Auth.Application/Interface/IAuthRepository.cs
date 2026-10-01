using Auth.Domain.Entities;

namespace Auth.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task<bool> SaveChangesAsync();
    }
}