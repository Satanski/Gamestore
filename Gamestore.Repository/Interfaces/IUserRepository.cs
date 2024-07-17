using Gamestore.DAL.Entities;

namespace Gamestore.DAL.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByNameAndPassword(string name, string password);
}
