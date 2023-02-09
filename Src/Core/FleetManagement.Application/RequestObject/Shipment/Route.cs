using FleetManagement.Domain.Enums;

namespace FleetManagement.Application.RequestObject.Shipment;

public class Route
{
    public DeliveryPoints DeliveryPoint { get; set; }
    public ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();

    public IEnumerable<Delivery> Packages => Deliveries.Where(x => x.Barcode.StartsWith("P"));
    public IEnumerable<Delivery> Bags => Deliveries.Where(x => x.Barcode.StartsWith("C"));
}
