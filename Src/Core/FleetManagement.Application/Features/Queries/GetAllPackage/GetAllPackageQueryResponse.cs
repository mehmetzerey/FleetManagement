using FleetManagement.Domain.Entities;

namespace FleetManagement.Application.Features.Queries.GetAllPackage;

public class GetAllPackageQueryResponse
{
    public List<PackageModel> Result { get; private set; }

    public GetAllPackageQueryResponse(List<PackageModel> packages)
    {
        Result = packages;
    }
}
