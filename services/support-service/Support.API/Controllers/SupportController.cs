using Microsoft.AspNetCore.Mvc;
using Support.Application.DTOs;
using Support.Application.Services;

namespace Support.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SupportController : ControllerBase
    {
        private readonly ISupportService _supportService;

        public SupportController(ISupportService supportService)
        {
            _supportService = supportService;
        }

        [HttpPost("tickets")]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicketDto dto)
        {
            var ticket = await _supportService.CreateTicketAsync(dto);
            return Ok(ticket);
        }

        [HttpGet("tickets/{customerId}")]
        public async Task<IActionResult> GetCustomerTickets(Guid customerId)
        {
            var tickets = await _supportService.GetCustomerTicketsAsync(customerId);
            return Ok(tickets);
        }
    }
}