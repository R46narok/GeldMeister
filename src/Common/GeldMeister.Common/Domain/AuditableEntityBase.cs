namespace GeldMeister.Common.Domain;

public abstract class AuditableEntityBase : EntityBase
{
    public DateTime CreatedOn { get; set; }

    public DateTime? LastModifiedOn { get; set; }


}
