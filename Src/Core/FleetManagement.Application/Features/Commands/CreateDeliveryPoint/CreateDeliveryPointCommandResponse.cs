namespace FleetManagement.Application.Features.Commands.CreateDeliveryPoint;

public class CreateDeliveryPointCommandResponse
{
    public bool Result { get; private set; }
    public CreateDeliveryPointCommandResponse(bool result)
    {
        Result = result;
    }
}
