namespace Gamestore.BLL.Models;

public record CommentModel
{
    public Guid? Id { get; set; }

    public string? Name { get; set; }

    public string Body { get; set; }

    public List<CommentModel> ChildComments { get; set; } = [];
}
