using Restaurant.Application.DTOs;
using Restaurant.Application.Interfaces;
using Restaurant.Domain.Entities;

namespace Restaurant.Application.Services;

public class CategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
    {
        var categories = await _categoryRepository.GetAllAsync();
        return categories.Select(c => new CategoryDto(c.Id, c.Name, c.RestaurantId));
    }

    public async Task<CategoryDto?> GetCategoryByIdAsync(Guid id)
    {
        var c = await _categoryRepository.GetByIdAsync(id);
        if (c == null) return null;

        return new CategoryDto(c.Id, c.Name, c.RestaurantId);
    }

    public async Task<Guid> CreateCategoryAsync(CreateCategoryDto dto)
    {
        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            RestaurantId = dto.RestaurantId,
            CreatedAt = DateTime.UtcNow
        };

        await _categoryRepository.AddAsync(category);
        return category.Id;
    }
}