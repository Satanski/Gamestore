using Gamestore.Repository.Entities;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.Tests.Helpers;

#pragma warning disable S3881 // "IDisposable" should be implemented correctly
public class DatabaseFixture : IDisposable
#pragma warning restore S3881 // "IDisposable" should be implemented correctly
{
    public DatabaseFixture()
    {
        var options = new DbContextOptionsBuilder().
            UseSqlite("Data Source = testDb.db")
            .Options;

        Context = new GamestoreContext(options);

        Context.Database.EnsureDeleted();
        Context.Database.EnsureCreated();
    }

    public GamestoreContext Context { get; private set; }

    public void Dispose()
    {
        Context.Dispose();
        GC.SuppressFinalize(this);
    }
}
