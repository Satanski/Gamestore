using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Entities;

[Table("UserRoles")]
[PrimaryKey(nameof(UserId), nameof(RoleId))]
public class UserRole
{
    public Guid UserId { get; set; }

    public Guid RoleId { get; set; }

    public User User { get; set; }

    public Role Role { get; set; }
}
