namespace FleetManagement.Application.Features.Queries.GetAllBag;

public class GetAllBagQueryResponse
{
    public List<BagModel> Bags { get; private set; }

    public GetAllBagQueryResponse( List<BagModel> bagModels)
    {
        Bags = bagModels;
    }
}
