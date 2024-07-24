namespace Gamestore.BLL.Identity.Models;

public static class Permissions
{
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
        { PermissionAddComment!, PermissionValueAddComment },
        { PermissionDeleteComment!, PermissionValueDeleteComment },
        { PermissionBuyGame!, PermissionValueBuyGame },
        { PermissionManageUsers!, PermissionValueManageUsers },
        { PermissionManageRoles!, PermissionValueManageRoles },
        { PermissionDeletedGames!, PermissionValueDeletedGames },
        { PermissionManageEntities!, PermissionValueManageEntities },
        { PermissionEditOrders!, PermissionValueEditOrders },
        { PermissionOrderHistory!, PermissionValueOrderHistory },
        { PermissionOrderStatus!, PermissionValueOrderStatus },
        { PermissionBanUsers!, PermissionValueBanUsers },
        { PermissionModerateComments!, PermissionValueModerateComments },
    };

    public static string PermissionAddComment { get; } = "permissions.add.comment";

    public static string PermissionDeleteComment { get; } = "permissions.delete.comment";

    public static string PermissionBuyGame { get; } = "permissions.buy.game";

    public static string PermissionManageUsers { get; } = "permissions.manage.users";

    public static string PermissionManageRoles { get; } = "permissions.manage.roles";

    public static string PermissionDeletedGames { get; } = "permissions.deleted.games";

    public static string PermissionManageEntities { get; } = "permissions.manage.entities";

    public static string PermissionEditOrders { get; } = "permissions.edit.orders";

    public static string PermissionOrderHistory { get; } = "permissions.order.history";

    public static string PermissionOrderStatus { get; } = "permissions.order.status";

    public static string PermissionBanUsers { get; } = "permissions.ban.users";

    public static string PermissionModerateComments { get; } = "permissions.moderate.comments";

    public static string PermissionManageEntitiesOrDeletedGames { get; } = "ManageEntitiesOrDeletedGames";

    public static Dictionary<string, string> PermissionList => PermissionListDictionary;
}
