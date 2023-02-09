namespace FleetManagement.Application.Features.Commands.CreatePackageToBag;

public class CreatePackagesToBagsCommandRequest : IRequest<CreatePackagesToBagsCommandResponse>
{
    public string PackageId { get; set; } = String.Empty;
    public string BagId { get; set; } = String.Empty;
}
