using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Gamestore.DAL.Entities;

public class IdentityDbContext(DbContextOptions<IdentityDbContext> options) : IdentityDbContext<AppUser, AppRole, string>(options)
{
}
