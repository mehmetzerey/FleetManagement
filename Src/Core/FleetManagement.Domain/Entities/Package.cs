namespace FleetManagement.Domain.Entities;

public class Package : BaseEntity, IAggregateRoot
{

    public string DeliveryPointId { get; private set; }

    public DeliveryPoint DeliveryPoint { get; private set; }

    public int VolumetricWeight { get; private set; }

    public PackageStatus PackageStatus { get; private set; }

    public PackagesToBags PackagesToBags { get; private set; }

    public Package()
    {

    }

    public Package( string Barcode, string DeliveryPointForUnloading, int VolumetricWeight)
    {
        Guard.Against.NullOrEmpty(Barcode, nameof(Barcode));
        Guard.Against.NullOrEmpty(DeliveryPointForUnloading, nameof(DeliveryPointForUnloading));
        Id = Barcode;
        DeliveryPointId = DeliveryPointForUnloading;
        this.VolumetricWeight = VolumetricWeight;
        PackageStatus = PackageStatus.Created;
    }

    public void UpdatePackageStatus( PackageStatus packageStatus) => PackageStatus = packageStatus;

    public Package AddPackageToBag(PackagesToBags packagesToBags)
    {
        PackagesToBags = packagesToBags;
        return this;
    }

}
