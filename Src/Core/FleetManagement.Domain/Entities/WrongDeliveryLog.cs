namespace FleetManagement.Domain.Entities;

public class WrongDeliveryLog
{
    public string DeliveryPointId { get; private set; }
    public string DeliveryId { get; private set; }

    public WrongDeliveryLog()
    {

    }

    public WrongDeliveryLog(string deliveryPointId, string deliveryId)
    {
        Guard.Against.NullOrEmpty(deliveryPointId, nameof(deliveryPointId));
        Guard.Against.NullOrEmpty(deliveryId, nameof(deliveryId));
        DeliveryPointId = deliveryPointId;
        DeliveryId = deliveryId;
    }
}
