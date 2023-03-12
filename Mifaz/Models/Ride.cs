using System.ComponentModel.DataAnnotations;

namespace Mifaz.Models;

public class Ride
{
    [Key] public int Id { get; set; }
    public int DriverId { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
    public DateTime Date { get; set; }
    public int MaxCount { get; set; }
    public double Cost { get; set; }
}