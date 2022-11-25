namespace Core.Entity.Dtos;

public class BaseEntityDto : IBaseEntityDto
{
    public Guid Id { get; set; }
    // bool Deleted { get; set; }
    // DateTimeOffset? CreatedDate { get; set; }
    // DateTimeOffset? UpdatedDate { get; set; }
}