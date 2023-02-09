namespace FleetManagement.Application.Features.Queries.GetAllPackagesToBags;

public class GetAllPackagesToBagsQueryHandler : IRequestHandler<GetAllPackagesToBagsQueryRequest, GetAllPackagesToBagsQueryResponse>
{
    private readonly IReadPackagesToBagsRepository _repository;

    public GetAllPackagesToBagsQueryHandler(IReadPackagesToBagsRepository repository)
    {
        _repository = repository;
    }

    public Task<GetAllPackagesToBagsQueryResponse> Handle(GetAllPackagesToBagsQueryRequest request, CancellationToken cancellationToken)
    {
        var result = _repository.GetAll();
        return Task.FromResult( new GetAllPackagesToBagsQueryResponse(result.Select(x => new PackagesToBagsModel
        {
            BagId = x.BagId,
            PackageId = x.PackageId
        }).ToList()));
    }
}
