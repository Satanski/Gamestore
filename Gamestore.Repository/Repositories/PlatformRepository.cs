using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class PlatformRepository(GamestoreContext context) : IPlatformRepository
{
    private readonly GamestoreContext _context = context;

    public async Task<List<Game>> GetGamesByPlatformAsync(Guid id)
    {
        return await _context.GamePlatforms.Where(x => x.PlatformId == id).Include(x => x.Game).Select(x => x.Game).ToListAsync();
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

    public async Task AddAsync(Platform entity)
    {
        await _context.Platforms.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var platform = await _context.Platforms.FindAsync(id);

        if (platform != null)
        {
            _context.Platforms.Remove(platform);
        }
    }

    public async Task<List<Platform>> GetAllAsync()
    {
        return await _context.Platforms.ToListAsync();
    }

    public async Task<Platform?> GetByIdAsync(Guid id)
    {
        return await _context.Platforms.Where(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task UpdateAsync(Platform entity)
    {
        var g = await _context.Platforms.Where(p => p.Id == entity.Id).FirstAsync();
        _context.Entry(g).CurrentValues.SetValues(entity);
    }
}
