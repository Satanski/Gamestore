﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Gamestore.Repository.Entities;

[Table("Genres")]
[Index(nameof(Name), IsUnique = true)]
public class Genre
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    public string Name { get; set; }

    public Guid? ParentGenreId { get; set; } = null!;

    public ICollection<GameGenre> GameGenres { get; set; }
}
