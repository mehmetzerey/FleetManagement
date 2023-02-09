namespace FleetManagement.Persistence.Data.Config;

public class BagConfiguration : IEntityTypeConfiguration<Bag>
{
    public void Configure(EntityTypeBuilder<Bag> builder)
    {
        var navigation = builder.Metadata.FindNavigation(nameof(Bag.PackagesToBags));
        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}
