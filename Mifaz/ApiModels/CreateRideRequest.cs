namespace Mifaz.ApiModels;

public class CreateRideRequest
{
    public int DriverId { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
}