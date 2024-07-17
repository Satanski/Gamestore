using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Entities;

[Table("Roles")]
[Index(nameof(RoleName), IsUnique = true)]
public class Role
{
    public Guid Id { get; set; }

    public string RoleName { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}
