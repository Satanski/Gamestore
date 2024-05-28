using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.Tests.EntityConfigurations;

internal class GameGenreConfiguration : IEntityTypeConfiguration<GameGenre>
{
    public void Configure(EntityTypeBuilder<GameGenre> builder)
    {
        builder.HasOne(x => x.Game).WithMany(x => x.GameGenres).OnDelete(DeleteBehavior.Restrict);
    }
}
