namespace Gamestore.BLL.Identity.Models;

public record UserDto
{
    public UserModel User { get; set; }

    public List<string> Roles { get; set; }

    public string Password { get; set; }
}
