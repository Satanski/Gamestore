namespace Gamestore.BLL.Models;

public record PublisherModel
{
    public Guid Id { get; set; }

    public string CompanyName { get; set; }

    public string? HomePage { get; set; }

    public string? Description { get; set; }
}
