using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Logistics.API.Hubs;

public class LocationHub : Hub
{
    private readonly ILogger<LocationHub> _logger;

    public LocationHub(ILogger<LocationHub> logger)
    {
        _logger = logger;
    }

   
    public async Task UpdateLocation(Guid riderId, double latitude, double longitude)
    {
        _logger.LogInformation($"[SignalR] Rider {riderId} location updated: Lat={latitude}, Lng={longitude}");

       
        await Clients.All.SendAsync("ReceiveLocationUpdate", riderId, latitude, longitude);
    }
}