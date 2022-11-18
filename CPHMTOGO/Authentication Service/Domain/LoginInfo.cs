using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;

namespace AuthenticationService.Domain;

[Table("LoginInfo",Schema = "public"), MessagePackObject(keyAsPropertyName: true)]
public class LoginInfo
{
    [Column("id"), DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    [Column("username")] public string Username { get; init; } = default!;
    [Column("salt")] public byte[] PasswordSalt { get; set; } = default!;

    [Column("passwordHash")] public byte[] PasswordHash { get; set; } = default!;
    [Column("email")] public string Email { get; init; } = default!;
    
}