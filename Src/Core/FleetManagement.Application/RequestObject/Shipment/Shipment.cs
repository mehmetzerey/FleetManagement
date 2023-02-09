namespace FleetManagement.Application.RequestObject.Shipment;

public class Shipment
{
    public string Plate { get; set; } = String.Empty;
    public ICollection<Route> Route { get; set; } = new List<Route>();
}
