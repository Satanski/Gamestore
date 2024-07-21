using Gamestore.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Gamestore.DAL.EntityConfigurations;

internal class GameGenreConfiguration : IEntityTypeConfiguration<GameGenres>
{
    public void Configure(EntityTypeBuilder<GameGenres> builder)
    {
        builder.HasOne(x => x.Product).WithMany(x => x.ProductCategories).OnDelete(DeleteBehavior.Restrict);
    }
}
