using DiscriminatorSamples.Business.Interfaces;

namespace DiscriminatorSamples.Business;

public abstract class Person : AuditableBaseEntity, IAuditableEntity
{
    public string Name { get; set; }
    public Address Address { get; set; }
}
