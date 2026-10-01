using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.DTOs;
using Restaurant.Application.Services;

namespace Restaurant.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MenuItemController : ControllerBase
{
    private readonly MenuItemService _menuItemService;

    public MenuItemController(MenuItemService menuItemService)
    {
        _menuItemService = menuItemService;
    }

    [HttpGet("restaurant/{restaurantId:guid}")]
    public async Task<IActionResult> GetByRestaurantId(Guid restaurantId)
    {
        var items = await _menuItemService.GetMenuItemsByRestaurantIdAsync(restaurantId);
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var item = await _menuItemService.GetMenuItemByIdAsync(id);
        if (item == null)
            return NotFound(new { message = "Menu item not found." });

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMenuItemDto dto)
    {
        var itemId = await _menuItemService.CreateMenuItemAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = itemId }, new { id = itemId, message = "Menu item created successfully!" });
    }
}