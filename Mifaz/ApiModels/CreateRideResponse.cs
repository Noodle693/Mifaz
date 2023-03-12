using Mifaz.Models;

namespace Mifaz.ApiModels;

public class CreateRideResponse
{
    public CreateRideResponse(Ride ride)
    {
        Ride = ride;
    }

    public Ride Ride { get; }
}