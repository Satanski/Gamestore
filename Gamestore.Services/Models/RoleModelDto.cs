namespace Gamestore.BLL.Models;

public record RoleModelDto
{
    public RoleModel Role { get; set; }

    public List<string>? Permissions { get; set; }
}
