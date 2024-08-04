using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gamestore.DAL.Entities;

[Table("Games")]
[Index(nameof(Key), IsUnique = true)]
public class Game
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Key { get; set; }

    [Required]
    public double Price { get; set; }

    [Required]
    public int UnitInStock { get; set; }

    [Required]
    public int Discount { get; set; }

    [Required]
    public Guid PublisherId { get; set; }

    [Required]
    public DateOnly PublishDate { get; set; }

    public string? Description { get; set; } = null!;

    public int NumberOfViews { get; set; }

    public int? ReorderLevel { get; set; }

    public string? QuantityPerUnit { get; set; }

    public int? UnitsOnOrder { get; set; }

    public bool IsDeleted { get; set; }

    public Publisher Publisher { get; set; }

    public List<GameGenres> ProductCategories { get; set; }

    public List<GamePlatform> ProductPlatforms { get; set; }

    public List<OrderGame> OrderProducts { get; set; }

    public List<Comment> Comments { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj is Game other)
        {
            return Id.Equals(other.Id) && Key.Equals(other.Key, StringComparison.OrdinalIgnoreCase);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name.ToLowerInvariant());
    }
}
