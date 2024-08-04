namespace Gamestore.BLL.Models;

public record CustomerDto
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime BannedTill { get; set; }
}
