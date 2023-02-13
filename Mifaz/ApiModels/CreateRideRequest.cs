namespace Mifaz.ApiModels;

public class CreateRideRequest
{
    public int DriverId { get; set; }
    public double Price { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public string StartingCity { get; set; }
    public string DestinationCity { get; set; }
}