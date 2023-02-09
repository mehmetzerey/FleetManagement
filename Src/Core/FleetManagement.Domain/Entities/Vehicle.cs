namespace FleetManagement.Domain.Entities;

public class Vehicle : BaseEntity
{
    public Vehicle()
    {

    }

    public Vehicle( string LicencePlate)
    {
        Guard.Against.NullOrEmpty(LicencePlate, nameof(LicencePlate));

        Id = LicencePlate;
    }
}
