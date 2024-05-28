using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.DAL.EntityConfigurations;

internal class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
{
    public void Configure(EntityTypeBuilder<GameGenre> builder)
    {
        builder.HasOne(x => x.Game).WithMany(x => x.GameGenres).OnDelete(DeleteBehavior.Restrict);
    }
}
