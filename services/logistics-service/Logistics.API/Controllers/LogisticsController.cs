using Microsoft.AspNetCore.Mvc;
using Logistics.Application.Interfaces;
using Logistics.Domain.Entities;

namespace Logistics.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogisticsController : ControllerBase
{
    private readonly IDeliveryRepository _deliveryRepository;
    private readonly IRiderRepository _riderRepository;

    public LogisticsController(IDeliveryRepository deliveryRepository, IRiderRepository riderRepository)
    {
        _deliveryRepository = deliveryRepository;
        _riderRepository = riderRepository;
    }

   
    [HttpPost("riders")]
    public async Task<IActionResult> AddRider([FromBody] CreateRiderDto request)
    {
        var rider = new Rider
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            PhoneNumber = request.PhoneNumber
        };

        await _riderRepository.AddAsync(rider);
        await _riderRepository.SaveChangesAsync();

        return Ok(rider);
    }

   
    [HttpGet("riders/available")]
    public async Task<IActionResult> GetAvailableRiders()
    {
        var riders = await _riderRepository.GetAvailableRidersAsync();
        return Ok(riders);
    }


    [HttpPut("deliveries/{deliveryId}/assign/{riderId}")]
    public async Task<IActionResult> AssignRider(Guid deliveryId, Guid riderId)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
        if (delivery == null) return NotFound("Delivery not found");

        var rider = await _riderRepository.GetByIdAsync(riderId);
        if (rider == null) return NotFound("Rider not found");

        
        delivery.AssignRider(riderId);
        rider.SetAvailability(false); 

        await _deliveryRepository.UpdateAsync(delivery);
        await _riderRepository.UpdateAsync(rider);
        await _deliveryRepository.SaveChangesAsync();

        return Ok(new { Message = "Rider assigned successfully", Delivery = delivery });
    }

    
    [HttpPut("deliveries/{deliveryId}/status")]
    public async Task<IActionResult> UpdateDeliveryStatus(Guid deliveryId, [FromBody] UpdateStatusDto request)
    {
        var delivery = await _deliveryRepository.GetByIdAsync(deliveryId);
        if (delivery == null) return NotFound("Delivery not found");

        if (request.Status.Equals("PickedUp", StringComparison.OrdinalIgnoreCase))
        {
            delivery.MarkAsPickedUp();
        }
        else if (request.Status.Equals("Delivered", StringComparison.OrdinalIgnoreCase))
        {
            delivery.MarkAsDelivered();

           
            if (delivery.RiderId.HasValue)
            {
                var rider = await _riderRepository.GetByIdAsync(delivery.RiderId.Value);
                if (rider != null)
                {
                    rider.SetAvailability(true);
                    await _riderRepository.UpdateAsync(rider);
                }
            }
        }

        await _deliveryRepository.UpdateAsync(delivery);
        await _deliveryRepository.SaveChangesAsync();

        return Ok(new { Message = $"Delivery status updated to {request.Status}", Delivery = delivery });
    }
}


public class CreateRiderDto
{
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}

public class UpdateStatusDto
{
    public string Status { get; set; } = string.Empty;
}