namespace FleetManagement.Application.Features.Queries.GetAllVehicle;

public class GetAllVehicleQueryResponse
{
    public List<VehicleModel> Vehicles { get; private set; }
    public GetAllVehicleQueryResponse(List<VehicleModel> vehicleModels)
    {
        Vehicles = vehicleModels;
    }
}
