namespace FleetManagement.Persistence.Data.Config;

public class WrongDeliveryLogConfiguration : IEntityTypeConfiguration<WrongDeliveryLog>
{
    public void Configure(EntityTypeBuilder<WrongDeliveryLog> builder)
    {
        builder.HasKey(pb => new { pb.DeliveryId, pb.DeliveryPointId});
    }
}
