using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CreateOrder;
using Ordering.Application.Features.Orders.Commands.UpdateOrderStatus;
using Ordering.Application.Features.Orders.Commands.CancelOrder;
using Ordering.Application.Features.Orders.Queries.GetOrderById;
using System;
using System.Threading.Tasks;

namespace Ordering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IMediator _mediator;

    public OrdersController(IMediator mediator)
    {
        _mediator = mediator;
    }

   
    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand command)
    {
        var orderId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetOrder), new { id = orderId }, orderId);
    }

    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var order = await _mediator.Send(new GetOrderByIdQuery(id));
        return Ok(order);
    }

    [HttpPut("status")]
    public async Task<IActionResult> UpdateStatus([FromBody] UpdateOrderStatusCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    
    [HttpDelete("{id}")]
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        var result = await _mediator.Send(new CancelOrderCommand(id));
        return Ok(result);
    }
}