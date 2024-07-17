namespace Gamestore.BLL.Models;

public record LoginModel
{
    public string Login { get; set; }

    public string Password { get; set; }

    public bool InternalAuth { get; set; }
}
