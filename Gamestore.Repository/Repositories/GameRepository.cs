using Gamestore.Repository.Entities;
using Gamestore.Repository.Interfaces;

namespace Gamestore.Repository.Repositories;

public class GameRepository(GamestoreContext context) : IGameRepository
{
    private readonly GamestoreContext _context = context;

    public Task<IEnumerable<Game>> GetAllGamesAsync()
    {
        var task = Task.Run(() => _context.Games.AsEnumerable());

        return task;
    }

    public Task<IEnumerable<Genre>> GetGenresByGameAsync(Guid gameId)
    {
        var task = Task.Run(() =>
        {
            var genres = from g in _context.Genres
                         join gg in _context.GameGenres on g.Id equals gg.GenreId
                         where gg.GameId == gameId
                         select new { g.Id, g.Name, g.ParentGenreId };

            List<Genre> result = [];
            foreach (var g in genres)
            {
                result.Add(new Genre() { Id = g.Id, Name = g.Name, ParentGenreId = g.ParentGenreId });
            }

            return result.AsEnumerable();
        });

        return task;
    }

    public Task<IEnumerable<Platform>> GetPlatformsByGameAsync(Guid gameId)
    {
        var task = Task.Run(() =>
        {
            var platforms = from p in _context.Platforms
                            join gp in _context.GamePlatforms on p.Id equals gp.PlatformId
                            where gp.GameId == gameId
                            select new { p.Id, p.Type };

            List<Platform> result = [];
            foreach (var p in platforms)
            {
                result.Add(new Platform() { Id = p.Id, Type = p.Type });
            }

            return result.AsEnumerable();
        });

        return task;
    }

    public Task<Game?> GetGameByIdAsync(Guid gameId)
    {
        var task = Task.Run(() => _context.Games.Where(x => x.Id == gameId).FirstOrDefault());

        return task;
    }

    public Task<Game?> GetGameByKeyAsync(string key)
    {
        var task = Task.Run(() => _context.Games.Where(x => x.Key == key).FirstOrDefault());

        return task;
    }

    public Task AddGameAsync(Game game)
    {
        Task task = Task.Run(() =>
        {
            _context.Games.Add(game);

            foreach (var item in game.GameGenres)
            {
                _context.GameGenres.Add(item);
            }

            foreach (var item in game.GamePlatforms)
            {
                _context.GamePlatforms.Add(item);
            }

            _context.SaveChangesAsync();
        });

        return task;
    }

    public Task UpdateGameAsync(Game game)
    {
        var task = Task.Run(() =>
        {
            var g = _context.Games.Where(p => p.Id == game.Id).First();
            _context.Entry(g).CurrentValues.SetValues(game);

            var gameGenres = _context.GameGenres.Where(x => x.GameId == game.Id);
            _context.GameGenres.RemoveRange(gameGenres);

            var gamePlatforms = _context.GamePlatforms.Where(x => x.GameId == game.Id);
            _context.GamePlatforms.RemoveRange(gamePlatforms);

            _context.SaveChanges();

            foreach (var item in game.GameGenres)
            {
                _context.GameGenres.Add(item);
            }

            foreach (var item in game.GamePlatforms)
            {
                _context.GamePlatforms.Add(item);
            }

            _context.SaveChanges();
        });

        return task;
    }

    public Task DeleteGameAsync(Guid gameId)
    {
        var task = Task.Run(() =>
        {
            var game = _context.Games.Find(gameId);

            if (game != null)
            {
                var gameGenres = _context.GameGenres.Where(x => x.GameId == gameId);
                _context.GameGenres.RemoveRange(gameGenres);

                var gamePlatforms = _context.GamePlatforms.Where(x => x.GameId == gameId);
                _context.GamePlatforms.RemoveRange(gamePlatforms);

                _context.Games.Remove(game);

                _context.SaveChanges();
            }
        });

        return task;
    }
}
