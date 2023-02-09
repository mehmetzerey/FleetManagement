namespace FleetManagement.Application.Features.Queries.GetAllPackagesToBags;

public class GetAllPackagesToBagsQueryResponse
{
    public List<PackagesToBagsModel> PackagesToBags { get; private set; }

    public GetAllPackagesToBagsQueryResponse(List<PackagesToBagsModel> packagesToBags)
    {
        PackagesToBags = packagesToBags;
    }
}
