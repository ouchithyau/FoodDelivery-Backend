using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.DTOs;
using Restaurant.Application.Services;

namespace Restaurant.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantController : ControllerBase
{
    private readonly RestaurantService _restaurantService;

    public RestaurantController(RestaurantService restaurantService)
    {
        _restaurantService = restaurantService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var restaurants = await _restaurantService.GetAllRestaurantsAsync();
        return Ok(restaurants);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var restaurant = await _restaurantService.GetRestaurantByIdAsync(id);
        if (restaurant == null)
            return NotFound(new { message = "Restaurant not found." });

        return Ok(restaurant);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRestaurantDto dto)
    {
        var restaurantId = await _restaurantService.CreateRestaurantAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = restaurantId }, new { id = restaurantId, message = "Restaurant created successfully!" });
    }

    [HttpPatch("{id:guid}/toggle-status")]
    public async Task<IActionResult> ToggleStatus(Guid id)
    {
        await _restaurantService.ToggleRestaurantStatusAsync(id);
        return Ok(new { message = "Restaurant status updated successfully." });
    }
}