using DiscriminatorSamples.Business.Interfaces;

namespace DiscriminatorSamples.Business;

public class AuditableBaseEntity : BaseEntity, IAuditableEntity
{
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }
    public string CreatedBy { get; set; }
    public string? LastModifiedBy { get; set; }
}

public class BaseEntity : IEntity
{
    public Guid Id { get; set; }
}
