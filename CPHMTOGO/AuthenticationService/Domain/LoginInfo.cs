namespace AuthenticationService.Domain;

using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;

[Table("LoginInfo")]
[MessagePackObject(keyAsPropertyName:true)]
public class LoginInfo
{
    [Column("id")]
    [System.ComponentModel.DataAnnotations.Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    [Column("username")]
    public string Username { get; set; }
    [Column("salt")]
    public byte[] PasswordSalt { get; set; }
    [Column("passwordHash")]

    public byte[] PasswordHash { get; set; }  
    [Column("email")]
    public string Email { get; set; }
}