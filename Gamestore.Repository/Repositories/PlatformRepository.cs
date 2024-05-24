using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;

namespace Gamestore.DAL.Repositories;

public class PlatformRepository(GamestoreContext context) : IPlatformRepository
{
    private readonly GamestoreContext _context = context;

    public Task<IEnumerable<Game>> GetGamesByPlatformAsync(Guid platformId)
    {
        var task = Task.Run(() =>
        {
            var games = from g in _context.Games
                        where g.GamePlatforms.Any(x => x.PlatformId == platformId)
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

    public Task<Platform?> GetPlatformByIdAsync(Guid platformId)
    {
        var task = Task.Run(() => _context.Platforms.Where(x => x.Id == platformId).FirstOrDefault());

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

    public Task DeletePlatformAsync(Guid platformId)
    {
        var task = Task.Run(() =>
        {
            var platform = _context.Platforms.Find(platformId);

            if (platform != null)
            {
                _context.Platforms.Remove(platform);

                _context.SaveChanges();
            }
        });

        return task;
    }
}
