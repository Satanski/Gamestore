using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gamestore.DAL.Entities;

[Table("Categories")]
[Index(nameof(Name), IsUnique = true)]
public class Category
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public Guid? ParentCategoryId { get; set; } = null!;

    public ICollection<ProductCategory> ProductGenres { get; set; }
}
