namespace Logistics.Domain.Entities;

public class Rider
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;

    // GPS Tracking 
    public double CurrentLatitude { get; private set; }
    public double CurrentLongitude { get; private set; }
    public bool IsAvailable { get; private set; } = true;

    // Domain Logic: Location 
    public void UpdateLocation(double latitude, double longitude)
    {
        CurrentLatitude = latitude;
        CurrentLongitude = longitude;
    }

    // Domain Logic:  Status  (Available/Busy)
    public void SetAvailability(bool isAvailable)
    {
        IsAvailable = isAvailable;
    }
}