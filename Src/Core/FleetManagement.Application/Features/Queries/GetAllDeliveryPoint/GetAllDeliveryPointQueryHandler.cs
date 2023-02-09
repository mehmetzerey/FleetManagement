using FleetManagement.Application.Interfaces.Repositories.DeliveryPoint;

namespace FleetManagement.Application.Features.Queries.GetAllDeliveryPoint;

public class GetAllDeliveryPointQueryHandler : IRequestHandler<GetAllDeliveryPointQueryRequest, GetAllDeliveryPointQueryResponse>
{
    private readonly IReadDeliveryPointRepository _readDeliveryPointRepository;

    public GetAllDeliveryPointQueryHandler(IReadDeliveryPointRepository readDeliveryPointRepository)
    {
        _readDeliveryPointRepository = readDeliveryPointRepository;
    }

    public Task<GetAllDeliveryPointQueryResponse> Handle(GetAllDeliveryPointQueryRequest request, CancellationToken cancellationToken)
    {
        var result = _readDeliveryPointRepository.GetAll();
        return Task.FromResult(new GetAllDeliveryPointQueryResponse(
                result.Select(x=> new DeliveryPointModel
                {
                    DeliveryPointName = x.DeliveryPointName,
                    Value = x.Id
                })
                .ToList()
            ));
    }
}
