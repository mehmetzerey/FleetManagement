namespace FleetManagement.Application.Features.Commands.CreateBag;

public class CreateBagCommandRequest : IRequest<CreateBagCommandResponse>
{
    public string Barcode { get; set; } = String.Empty;
    public string DeliveryPointId { get; set; }
}
