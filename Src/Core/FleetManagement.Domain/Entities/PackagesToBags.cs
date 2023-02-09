using FleetManagement.Domain.Enums;

namespace FleetManagement.Domain.Entities;

public class PackagesToBags
{
    public string PackageId { get; set; }
    public Package Package { get; set; }

    public string BagId { get; set; }
    public Bag Bag { get; set; }
}
