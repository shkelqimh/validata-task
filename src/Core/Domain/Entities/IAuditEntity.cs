namespace Domain.Entities;

public interface IAuditEntity
{
    DateTime CreatedOn { get; }
    DateTime? ModifiedOn { get; }
}