namespace Gamestore.BLL.Models;

public record CommentModelDto
{
    public CommentModel Comment { get; set; }

    public Guid? ParentId { get; set; }

    public string? Action { get; set; }
}
