using Gamestore.Repository.Entities;
using Gamestore.Repository.Interfaces;

namespace Gamestore.Repository.Repositories;

public class GenreRepository(GamestoreContext context) : IGenreRepository
{
    private readonly GamestoreContext _context = context;

    public Task AddGenreAsync(Genre genre)
    {
        Task task = Task.Run(() =>
        {
            _context.Genres.Add(genre);

            _context.SaveChangesAsync();
        });

        return task;
    }

    public Task DeleteGenreAsync(Guid id)
    {
        var task = Task.Run(() =>
        {
            var genre = _context.Genres.Find(id);

            if (genre != null)
            {
                _context.Genres.Remove(genre);

                var gameGenres = _context.GameGenres.Where(x => x.GenreId == id);
                _context.GameGenres.RemoveRange(gameGenres);

                _context.SaveChanges();
            }
        });

        return task;
    }

    public Task<IEnumerable<Genre>> GetAllGenresAsync()
    {
        var task = Task.Run(() => _context.Genres.AsEnumerable());

        return task;
    }

    public Task<IEnumerable<Game>> GetGamesByGenreAsync(Guid id)
    {
        var task = Task.Run(() =>
        {
            var games = from g in _context.Games
                        where g.GameGenres.Any(x => x.GenreId == id)
                        select g;

            return games.AsEnumerable();
        });

        return task;
    }

    public Task<IEnumerable<Genre>> GetGenresByParentGenreAsync(Guid id)
    {
        var task = Task.Run(() => _context.Genres.Where(x => x.ParentGenreId == id).AsEnumerable());

        return task;
    }

    public Task<Genre?> GetGenreByIdAsync(Guid id)
    {
        var task = Task.Run(() => _context.Genres.Where(x => x.Id == id).FirstOrDefault());

        return task;
    }

    public Task UpdateGenreAsync(Genre genre)
    {
        var task = Task.Run(() =>
        {
            var g = _context.Genres.Where(p => p.Id == genre.Id).First();
            _context.Entry(g).CurrentValues.SetValues(genre);

            _context.SaveChanges();
        });

        return task;
    }
}
