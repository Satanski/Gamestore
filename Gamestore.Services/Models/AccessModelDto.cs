namespace Gamestore.BLL.Models;

public record AccessModelDto
{
    public string TargetPage { get; set; }

    public string? TargetId { get; set; }
}
