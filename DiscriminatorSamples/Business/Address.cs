namespace DiscriminatorSamples.Business;
public class Address : AuditableBaseEntity
{
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }

}
