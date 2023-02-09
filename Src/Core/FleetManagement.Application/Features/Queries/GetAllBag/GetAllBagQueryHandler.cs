using FleetManagement.Application.Interfaces.Repositories.Bag;
using FleetManagement.Application.Interfaces.Repositories.Specifications;

namespace FleetManagement.Application.Features.Queries.GetAllBag;

public class GetAllBagQueryHandler : IRequestHandler<GetAllBagQueryRequest, GetAllBagQueryResponse>
{
    private readonly IReadBagRepository _readBagRepository;
    public GetAllBagQueryHandler(IReadBagRepository readBagRepository)
    {
        _readBagRepository = readBagRepository;
    }

    public Task<GetAllBagQueryResponse> Handle(GetAllBagQueryRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult (new GetAllBagQueryResponse(
            _readBagRepository
            .GetAll(tracking: false)
            .Select(x=> new BagModel { BagStatus = x.BagStatus, DeliveryPointId = x.DeliveryPointId, Barcode = x.Id})
            .ToList()
            ));
    }
}
