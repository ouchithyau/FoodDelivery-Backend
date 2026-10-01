namespace Restaurant.Application.Interfaces;

public interface IRestaurantRepository
{
    Task<IEnumerable<Domain.Entities.Restaurant>> GetAllAsync();
    Task<Domain.Entities.Restaurant?> GetByIdAsync(Guid id);
    Task AddAsync(Domain.Entities.Restaurant restaurant);
    Task UpdateAsync(Domain.Entities.Restaurant restaurant);
    Task DeleteAsync(Guid id);
}