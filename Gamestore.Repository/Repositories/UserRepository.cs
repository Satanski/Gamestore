using Gamestore.DAL.Entities;
using Gamestore.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Repositories;

public class UserRepository(GamestoreContext context) : IUserRepository
{
    public Task<User?> GetByNameAndPassword(string name, string password)
    {
        return context.Users.Where(x => x.UserName == name && x.Password == password).FirstOrDefaultAsync();
    }
}
