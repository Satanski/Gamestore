using Gamestore.BLL.Identity.Models;
using Gamestore.BLL.Models;
using Gamestore.BLL.Models.Notifications;
using Gamestore.IdentityRepository.Identity;
using Microsoft.AspNetCore.Identity;

namespace Gamestore.BLL.Interfaces;

public interface IUserService
{
    Task AddUserAsync(UserDto user);

    Task DeleteUserAsync(string userId);

    List<CustomerDto> GetAllUsers();

    Task<CustomerDto> GetUserByIdAsync(string userId);

    Task<List<UserRoleDto>> GetUserRolesByUserId(string userId);

    Task<string> LoginAsync(LoginModelDto login);

    Task<string> LoginInternalAsync(LoginModelDto login);

    Task<string> LoginExternalAsync(LoginModelDto login);

    Task<IdentityResult> UpdateUserAsync(UserDto user);

    IEnumerable<string> GetNotificationMethods();

    IEnumerable<string> GetUserNotificationMethods(AppUser user);

    Task SetUserNotificationMethodsAsync(NotificationsDto notificaltionList, AppUser user);
}
