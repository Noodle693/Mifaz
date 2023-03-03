using System.ComponentModel.DataAnnotations;

namespace Mifaz.Models;

public class Ride
{
    [Key] public int Id { get; set; }
    public int DriverId { get; set; }
    public double Price { get; set; }
    public DateTime Date { get; set; }
    public string Origin { get; set; }
    public string Destination { get; set; }
}