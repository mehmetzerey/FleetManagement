using FleetManagement.Application.Interfaces.Repositories.Vehicle;

namespace FleetManagement.Application.Features.Queries.GetAllVehicle;

public class GetAllVehicleQueryHandler : IRequestHandler<GetAllVehicleQueryRequest, GetAllVehicleQueryResponse>
{
    private readonly IReadVehicleRepository _readVehicleRepository;

    public GetAllVehicleQueryHandler(IReadVehicleRepository readVehicleRepository)
    {
        _readVehicleRepository = readVehicleRepository;
    }

    public Task<GetAllVehicleQueryResponse> Handle(GetAllVehicleQueryRequest request, CancellationToken cancellationToken)
    {
        var result = _readVehicleRepository.GetAll();
        return Task.FromResult(new GetAllVehicleQueryResponse(result.Select(x => new VehicleModel
        {
            LicencePlate = x.Id
        }).ToList()));
    }
}
