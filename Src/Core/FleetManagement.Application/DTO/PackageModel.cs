using FleetManagement.Domain.Enums;

namespace FleetManagement.Application.DTO;

public class PackageModel
{
    public string Barcode { get; set; } = String.Empty;
    public string DeliveryPointId { get; set; } = String.Empty;
    public int VolumetricWeight { get; set; }
    public PackageStatus PackageStatus { get; set; }
}
