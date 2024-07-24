namespace Gamestore.BLL.Identity.Models;

public static class Permissions
{
    private const string PermissionAddComment = "permissions.add.comment";
    private const string PermissionDeleteComment = "permissions.delete.comment";
    private const string PermissionBuyGame = "permissions.buy.game";
    private const string PermissionManageUsers = "permissions.manage.users";
    private const string PermissionManageRoles = "permissions.manage.roles";
    private const string PermissionDeletedGames = "permissions.deleted.games";
    private const string PermissionManageEntities = "permissions.manage.entities";
    private const string PermissionEditOrders = "permissions.edit.orders";
    private const string PermissionOrderHistory = "permissions.order.history";
    private const string PermissionOrderStatus = "permissions.order.status";
    private const string PermissionBanUsers = "permissions.ban.users";
    private const string PermissionModerateComments = "permissions.moderate.comments";
    private const string PermissionValueAddComment = "AddComment";
    private const string PermissionValueDeleteComment = "permissions.delete.comment";
    private const string PermissionValueBuyGame = "permissions.buy.game";
    private const string PermissionValueManageUsers = "permissions.manage.users";
    private const string PermissionValueManageRoles = "permissions.manage.roles";
    private const string PermissionValueDeletedGames = "permissions.deleted.games";
    private const string PermissionValueManageEntities = "permissions.manage.entities";
    private const string PermissionValueEditOrders = "permissions.edit.orders";
    private const string PermissionValueOrderHistory = "permissions.order.history";
    private const string PermissionValueOrderStatus = "permissions.order.status";
    private const string PermissionValueBanUsers = "permissions.ban.users";
    private const string PermissionValueModerateComments = "permissions.moderate.comments";

    private static readonly Dictionary<string, string> PermissionListDictionary = new()
    {
        { PermissionAddComment, PermissionValueAddComment },
        { PermissionDeleteComment, PermissionValueDeleteComment},
        { PermissionBuyGame, PermissionValueBuyGame },
        { PermissionManageUsers, PermissionValueManageUsers },
        { PermissionManageRoles, PermissionValueManageRoles },
        { PermissionDeletedGames, PermissionValueDeletedGames },
        { PermissionManageEntities, PermissionValueManageEntities },
        { PermissionEditOrders, PermissionValueEditOrders},
        { PermissionOrderHistory, PermissionValueOrderHistory },
        { PermissionOrderStatus, PermissionValueOrderStatus },
        { PermissionBanUsers, PermissionValueBanUsers},
        { PermissionModerateComments, PermissionValueModerateComments },
    };

    public static Dictionary<string, string> PermissionList => PermissionListDictionary;
}
