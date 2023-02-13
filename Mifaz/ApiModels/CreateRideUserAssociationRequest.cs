namespace Mifaz.ApiModels;

public class CreateRideUserAssociationRequest
{
    public int RideId { get; set; }
    public int PassengerId { get; set; }
}