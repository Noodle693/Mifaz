using Mifaz.ApiModels.ResponseModels;

namespace Mifaz.ApiModels;

public class GetRidesResponse
{
    public GetRidesResponse(List<ResponseRide> rides)
    {
        Rides = rides;
    }

    public List<ResponseRide> Rides { get; }
}