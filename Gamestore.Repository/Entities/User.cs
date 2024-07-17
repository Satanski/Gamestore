using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gamestore.DAL.Entities;

[Table("Users")]
[Index(nameof(UserName), IsUnique = true)]
public class User
{
    [Key]
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string Password { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}
