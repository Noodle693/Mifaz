namespace Mifaz.ApiModels.ResponseModels;

public class ResponseRide
{
    public string DriverFirstName { get; set; }
    public string DriverLastName { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime Date { get; set; }
    public int PassengerCount { get; set; }
    public int MaxCount { get; set; }
    public double Cost { get; set; }
}