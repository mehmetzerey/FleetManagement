namespace FleetManagement.Application.Features.Commands.CreatePackage;

public class CreatePackageCommandRequest : IRequest<CreatePackageCommandResponse>
{
    public string Barcode { get; set; } = String.Empty;
    public string DeliveryPointId { get; set; } = String.Empty;
    public int VolumetricWeight { get; set; }
}
