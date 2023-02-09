using FleetManagement.Application.Interfaces.Repositories.DeliveryPoint;

namespace FleetManagement.Application.Features.Commands.CreateDeliveryPoint;

public class CreateDeliveryPointCommandHandler : IRequestHandler<CreateDeliveryPointCommandRequest, CreateDeliveryPointCommandResponse>
{
    private readonly IWriteDeliveryPointRepository _writeDeliveryPointRepository;

    public CreateDeliveryPointCommandHandler(IWriteDeliveryPointRepository writeDeliveryPointRepository)
    {
        _writeDeliveryPointRepository = writeDeliveryPointRepository;
    }

    public async Task<CreateDeliveryPointCommandResponse> Handle(CreateDeliveryPointCommandRequest request, CancellationToken cancellationToken)
    {
        var result = await _writeDeliveryPointRepository.AddAsync(new(request.DeliveryPoint, request.Value));
        await _writeDeliveryPointRepository.SaveAsync();
        return new CreateDeliveryPointCommandResponse(result);
    }
}
