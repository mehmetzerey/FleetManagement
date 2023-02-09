namespace FleetManagement.Domain.Entities;

public class Bag : BaseEntity
{
    public string DeliveryPointId { get; private set; }
    public DeliveryPoint DeliveryPoint { get; private set; }

    private readonly List<PackagesToBags> _PackagesToBags = new List<PackagesToBags>();
    public IReadOnlyCollection<PackagesToBags> PackagesToBags => _PackagesToBags.AsReadOnly();

    public int TotalPackages => _PackagesToBags.Count();

    public BagStatus BagStatus { get; private set; }

    public Bag()
    {

    }

    public Bag(string barcode, string deliveryPointForUnloading)
    {
        Guard.Against.NullOrEmpty(barcode, nameof(barcode));
        Guard.Against.NullOrEmpty(deliveryPointForUnloading, nameof(deliveryPointForUnloading));
        DeliveryPointId = deliveryPointForUnloading;
        Id = barcode;
    }

    public void UpdateBagStatus(BagStatus bagStatus) => BagStatus = bagStatus;

    public Bag AddPackagesToBags(PackagesToBags packagesToBags)
    {
        _PackagesToBags.Add(packagesToBags);
        return this;
    }
}
