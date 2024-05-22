using Gamestore.Repository.Entities;
using Gamestore.Repository.Interfaces;

namespace Gamestore.Repository.Repositories;

public class PlatformRepository(GamestoreContext context) : IPlatformRepository
{
    private readonly GamestoreContext _context = context;

    public Task<IEnumerable<Game>> GetGamesByPlatformAsync(Guid platformId)
    {
        var task = Task.Run(() =>
        {
            var games = from g in _context.Games
                        where g.GamePlatforms.Any(x => x.Platform == platformId)
                        select g;

            return games.AsEnumerable();
        });

        return task;
    }

    public Task AddPlatformAsync(Platform platform)
    {
        Task task = Task.Run(() =>
        {
            _context.Platforms.Add(platform);

            _context.SaveChangesAsync();
        });

        return task;
    }

    public Task<Platform?> GetPlatformByIdAsync(Guid id)
    {
        var task = Task.Run(() => _context.Platforms.Where(x => x.Id == id).FirstOrDefault());

        return task;
    }

    public Task<IEnumerable<Platform>> GetAllPlatformsAsync()
    {
        var task = Task.Run(() => _context.Platforms.AsEnumerable());

        return task;
    }

    public Task UpdatePlatformAsync(Platform platform)
    {
        var task = Task.Run(() =>
        {
            var g = _context.Platforms.Where(p => p.Id == platform.Id).First();
            _context.Entry(g).CurrentValues.SetValues(platform);

            _context.SaveChanges();
        });

        return task;
    }

    public Task DeletePlatformAsync(Guid id)
    {
        var task = Task.Run(() =>
        {
            var platform = _context.Platforms.Find(id);

            if (platform != null)
            {
                _context.Platforms.Remove(platform);

                var gamePlatforms = _context.GamePlatforms.Where(x => x.Platform == id);
                _context.GamePlatforms.RemoveRange(gamePlatforms);

                _context.SaveChanges();
            }
        });

        return task;
    }
}
