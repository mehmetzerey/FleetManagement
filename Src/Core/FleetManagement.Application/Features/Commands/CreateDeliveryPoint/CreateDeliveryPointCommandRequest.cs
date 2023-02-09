namespace FleetManagement.Application.Features.Commands.CreateDeliveryPoint;

public class CreateDeliveryPointCommandRequest : IRequest<CreateDeliveryPointCommandResponse>
{
    public string DeliveryPoint { get; private set; }
    public string Value { get; private set; }

    public CreateDeliveryPointCommandRequest( string deliveryPoint, string value)
    {
        DeliveryPoint = deliveryPoint;
        Value = value;
    }
}
