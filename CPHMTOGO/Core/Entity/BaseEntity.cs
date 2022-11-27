using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entity;

public class BaseEntity :IBaseEntity
{
    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    // bool Deleted { get; set; }
    // DateTimeOffset? CreatedDate { get; set; }
    // DateTimeOffset? UpdatedDate { get; set; }
}