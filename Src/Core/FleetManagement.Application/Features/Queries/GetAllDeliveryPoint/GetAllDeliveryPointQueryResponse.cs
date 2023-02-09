namespace FleetManagement.Application.Features.Queries.GetAllDeliveryPoint;

public class GetAllDeliveryPointQueryResponse
{
    public List<DeliveryPointModel> DeliveryPoints { get; private set; }
    public GetAllDeliveryPointQueryResponse( List<DeliveryPointModel> deliveryPoints)
    {
        DeliveryPoints = deliveryPoints;
    }
}
