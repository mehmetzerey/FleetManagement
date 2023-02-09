namespace FleetManagement.Application.Features.Commands.CreateVehicle;

public class CreateVehicleCommandRequest : IRequest<CreateVehicleCommandResponse>
{
    public string LicencePlate { get; set; }
    public CreateVehicleCommandRequest(string licencePlate)
    {
        LicencePlate = licencePlate;
    }
}
