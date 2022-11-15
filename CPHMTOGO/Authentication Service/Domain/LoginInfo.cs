using System.ComponentModel.DataAnnotations.Schema;
using MessagePack;

namespace AuthenticationService.Domain;
[Table("Authentication"),MessagePackObject(keyAsPropertyName:true)]
public class LoginInfo
{
    [Column("Id"),System.ComponentModel.DataAnnotations.Key]
    public int Id { get; set; }
    [Column("Username")]
    public string Username { get; set; }
    [Column("PasswordSalt")]
    public byte[] PasswordSalt { get; set; }
    [Column("PasswordHash")]

    public byte[] PasswordHash { get; set; }  
    [Column("Email")]
    public string Email { get; set; }
}