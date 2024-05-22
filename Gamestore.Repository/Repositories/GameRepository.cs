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

    public Task<Game?> GetGameByIdAsync(Guid id)
    {
        var task = Task.Run(() => _context.Games.Where(x => x.Id == id).FirstOrDefault());

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

    public Task DeleteGameAsync(Guid id)
    {
        var task = Task.Run(() =>
        {
            var game = _context.Games.Find(id);

            if (game != null)
            {
                _context.Games.Remove(game);

                var gameGenres = _context.GameGenres.Where(x => x.GameId == id);
                _context.GameGenres.RemoveRange(gameGenres);

                var gamePlatforms = _context.GamePlatforms.Where(x => x.GameId == id);
                _context.GamePlatforms.RemoveRange(gamePlatforms);

                _context.SaveChanges();
            }
        });

        return task;
    }
}
