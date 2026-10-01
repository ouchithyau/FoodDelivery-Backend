using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.DTOs;
using Restaurant.Application.Services;

namespace Restaurant.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;

    public CategoryController(CategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var category = await _categoryService.GetCategoryByIdAsync(id);
        if (category == null)
            return NotFound(new { message = "Category not found." });

        return Ok(category);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
    {
        var categoryId = await _categoryService.CreateCategoryAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = categoryId }, new { id = categoryId, message = "Category created successfully!" });
    }
}