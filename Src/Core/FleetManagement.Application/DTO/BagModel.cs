using FleetManagement.Domain.Enums;

namespace FleetManagement.Application.DTO;

public class BagModel
{
    public string Barcode { get; set; }
    public string DeliveryPointId { get; set; } = String.Empty;
    public BagStatus BagStatus { get; set; }
}
