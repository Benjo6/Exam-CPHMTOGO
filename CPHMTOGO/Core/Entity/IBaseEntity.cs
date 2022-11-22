namespace Core.Entity;

public interface IBaseEntity
{
    Guid Id { get; set; }
    // bool Deleted { get; set; }
    // DateTimeOffset? CreatedDate { get; set; }
    // DateTimeOffset? UpdatedDate { get; set; }
}