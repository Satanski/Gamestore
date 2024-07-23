namespace Gamestore.BLL.Identity.Models;

public record ExternalLoginDto
{
    public string Email { get; set; }

    public string Password { get; set; }
}
