using FleetManagement.Domain.Entities;

namespace FleetManagement.Persistence.Data.Config;

public class PackagesToBagsConfiguration : IEntityTypeConfiguration<PackagesToBags>
{
    public void Configure(EntityTypeBuilder<PackagesToBags> builder)
    {
        builder.HasKey(pb => new { pb.PackageId, pb.BagId });

    }
}
