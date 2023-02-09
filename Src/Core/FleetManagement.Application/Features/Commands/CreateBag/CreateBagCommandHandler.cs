using FleetManagement.Application.Interfaces.Repositories.Bag;
using FleetManagement.Domain.Entities;

namespace FleetManagement.Application.Features.Commands.CreateBag;

public class CreateBagCommandHandler : IRequestHandler<CreateBagCommandRequest, CreateBagCommandResponse>
{
    readonly IWriteBagRepository _writeBagRepository;

    public CreateBagCommandHandler(IWriteBagRepository writeBagRepository)
    {
        _writeBagRepository = writeBagRepository;
    }

    public async Task<CreateBagCommandResponse> Handle(CreateBagCommandRequest request, CancellationToken cancellationToken)
    {
        await _writeBagRepository.AddAsync(new Bag(request.Barcode, request.DeliveryPointId));
        await _writeBagRepository.SaveAsync();
        return new();
    }
}
