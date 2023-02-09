namespace FleetManagement.Domain.Entities.Common;

public abstract class BaseEntity
{
    public virtual string Id { get; set; } = String.Empty;
}
