using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gamestore.DAL.Entities;

[Table("Comments")]
public class Comment
{
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Body { get; set; }

    [Required]
    public Guid GameId { get; set; }

    public Guid? ParentCommentId { get; set; }

    public Game Game { get; set; }

    public Comment? ParentComment { get; set; }
}
