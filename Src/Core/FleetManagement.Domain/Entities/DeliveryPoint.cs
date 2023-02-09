namespace FleetManagement.Domain.Entities;

public class DeliveryPoint : BaseEntity, IAggregateRoot
{
    public string DeliveryPointName { get; private set; } = String.Empty;

    public DeliveryPoint()
    {

    }
    public DeliveryPoint( string deliveryPoint, string value)
    {
        Guard.Against.NullOrEmpty(deliveryPoint, nameof(deliveryPoint));
        Guard.Against.NullOrEmpty(value, nameof(value));
        Id = value;
        DeliveryPointName = deliveryPoint;

    }
}
