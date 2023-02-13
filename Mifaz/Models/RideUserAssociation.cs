using System.ComponentModel.DataAnnotations;

namespace Mifaz.Models;

public class RideUserAssociation
{
    [Key] public int Id { get; set; }

    public int RideId { get; set; }

    public int PassengerId { get; set; }
}