using Gamestore.BLL.Identity.Models;
using Gamestore.BLL.Models;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Gamestore.BLL.Interfaces;

public interface IUserService
{
    Task AddUserAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, UserDto user);

    Task DeleteUserAsync(UserManager<AppUser> userManager, string userId);

    List<CustomerDto> GetAllUsers(UserManager<AppUser> userManager);

    Task<CustomerDto> GetUserByIdAsync(UserManager<AppUser> userManager, string userId);

    Task<List<UserRoleDto>> GetUserRolesByUserId(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, string userId);

    Task<string> LoginAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration, LoginModelDto login);

    Task<string> LoginInternalAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, IConfiguration configuration, LoginModelDto login);

    Task<string> LoginExternalAsync(RoleManager<AppRole> roleManager, IConfiguration configuration, LoginModelDto login);

    Task<IdentityResult> UpdateUserAsync(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, UserDto user);
}
