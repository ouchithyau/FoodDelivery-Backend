using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Carts.Commands.AddToCart;
using Ordering.Application.Features.Carts.Commands.RemoveFromCart;
using Ordering.Application.Features.Carts.Commands.ClearCart;
using Ordering.Application.Features.Carts.Queries.GetCart;
using System;
using System.Threading.Tasks;

namespace Ordering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CartsController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    
    [HttpGet("{customerId}")]
    public async Task<IActionResult> GetCart(Guid customerId)
    {
        var cart = await _mediator.Send(new GetCartQuery(customerId));
        return Ok(cart);
    }

   
    [HttpPost]
    public async Task<IActionResult> AddToCart([FromBody] AddToCartCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    
    [HttpDelete("{customerId}/items/{menuItemId}")]
    public async Task<IActionResult> RemoveFromCart(Guid customerId, Guid menuItemId)
    {
        var result = await _mediator.Send(new RemoveFromCartCommand(customerId, menuItemId));
        return Ok(result);
    }

    
    [HttpDelete("{customerId}")]
    public async Task<IActionResult> ClearCart(Guid customerId)
    {
        var result = await _mediator.Send(new ClearCartCommand(customerId));
        return Ok(result);
    }
}