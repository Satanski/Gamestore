using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gamestore.DAL.Entities;

[Table("Publishers")]
[Index(nameof(CompanyName), IsUnique = true)]
public class Publisher
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string CompanyName { get; set; }

    public string? HomePage { get; set; }

    public string? Description { get; set; }

    public ICollection<Game> Games { get; set; }
}
