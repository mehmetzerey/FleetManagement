using FleetManagement.Application.Interfaces.Repositories.Vehicle;

namespace FleetManagement.Application.Features.Commands.CreateVehicle;

public class CreateVehicleCommandHandler : IRequestHandler<CreateVehicleCommandRequest, CreateVehicleCommandResponse>
{
    private readonly IWriteVehicleRepository _writeVehicleRepository;

    public CreateVehicleCommandHandler(IWriteVehicleRepository writeVehicleRepository)
    {
        _writeVehicleRepository = writeVehicleRepository;
    }

    public async Task<CreateVehicleCommandResponse> Handle(CreateVehicleCommandRequest request, CancellationToken cancellationToken)
    {
        var resut = await _writeVehicleRepository.AddAsync(new(request.LicencePlate));
        await _writeVehicleRepository.SaveAsync();
        return new CreateVehicleCommandResponse { Result = resut};
    }
}
