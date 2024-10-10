namespace DiscriminatorSamples.Business.Interfaces;

public interface IAuditable
{
    DateTime CreatedAt { get; set; }
    DateTime? LastModifiedAt { get; set; }
    string CreatedBy { get; set; }
    string? LastModifiedBy { get; set; }
}