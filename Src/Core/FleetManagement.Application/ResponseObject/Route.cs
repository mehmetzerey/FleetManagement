using FleetManagement.Domain.Enums;

namespace FleetManagement.Application.ResponseObject;

public class Route
{
    public DeliveryPoints DeliveryPoint { get; set; }
    public ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
}
