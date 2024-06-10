namespace Gamestore.BLL.Models;

public record PublisherModelDto
{
    public string CompanyName { get; set; }

    public string? HomePage { get; set; }

    public string? Description { get; set; }
}
