using System.ComponentModel.DataAnnotations;

namespace Mifaz.Models;

public class Ride
{
    [Key] public int Id { get; set; }
    public int DriverId { get; set; }
    public double Price { get; set; }
    public DateTime StartingDate { get; set; }
    public DateTime EndingDate { get; set; }
    public string StartingCity { get; set; }
    public string DestinationCity { get; set; }
}