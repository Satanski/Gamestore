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

    public string? ContactName { get; set; }

    public string? ContactTitle { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public ICollection<Game> Games { get; set; }
}
