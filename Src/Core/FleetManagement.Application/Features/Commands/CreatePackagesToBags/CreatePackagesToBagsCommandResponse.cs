namespace FleetManagement.Application.Features.Commands.CreatePackageToBag;

public class CreatePackagesToBagsCommandResponse
{
    public bool Result { get; private set; }
    public CreatePackagesToBagsCommandResponse(bool result)
    {
        Result = result;
    }
}
