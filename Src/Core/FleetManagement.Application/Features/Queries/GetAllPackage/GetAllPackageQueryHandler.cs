using FleetManagement.Application.Interfaces.Repositories.Package;

namespace FleetManagement.Application.Features.Queries.GetAllPackage;

public class GetAllPackageQueryHandler : IRequestHandler<GetAllPackageQueryRequest, GetAllPackageQueryResponse>
{
    readonly IReadPackageRepository _readPackage;

    public GetAllPackageQueryHandler(IReadPackageRepository readPackage)
    {
        _readPackage = readPackage;
    }

    public Task<GetAllPackageQueryResponse> Handle(GetAllPackageQueryRequest request, CancellationToken cancellationToken)
    {
        var result = _readPackage.GetAll();
        return Task.FromResult(new GetAllPackageQueryResponse(result
            .Select(x=> new 
                PackageModel { 
                Barcode = x.Id, 
                DeliveryPointId = x.DeliveryPointId, 
                PackageStatus = x.PackageStatus, 
                VolumetricWeight = x.VolumetricWeight})
            .ToList()));
    }
}
