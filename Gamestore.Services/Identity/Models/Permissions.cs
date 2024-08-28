namespace Gamestore.BLL.Identity.Models;

public static class Permissions
{
    public const string PermissionValueAddComment = "AddComment";
    public const string PermissionValueDeleteComment = "DeleteComment";
    public const string PermissionValueBuyGame = "BuyGame";
    public const string PermissionValueManageUsers = "ManageUsers";
    public const string PermissionValueManageRoles = "ManageRoles";
    public const string PermissionValueDeletedGames = "DeletedGames";
    public const string PermissionValueManageEntities = "ManageEntities";
    public const string PermissionValueEditOrders = "EditOrders";
    public const string PermissionValueOrderHistory = "OrderHistory";
    public const string PermissionValueOrderStatus = "OrderStatus";
    public const string PermissionValueBanUsers = "BanUsers";
    public const string PermissionValueModerateComments = "ModerateComments";
    public const string PermissionValueManageEntitiesOrDeletedGames = "ManageEntitiesOrDeletedGames";

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

    public static Dictionary<string, string> PermissionList { get; } = new Dictionary<string, string>()
        {
            { PermissionAddComment, PermissionValueAddComment },
            { PermissionDeleteComment, PermissionValueDeleteComment },
            { PermissionBuyGame, PermissionValueBuyGame },
            { PermissionManageUsers, PermissionValueManageUsers },
            { PermissionManageRoles, PermissionValueManageRoles },
            { PermissionDeletedGames, PermissionValueDeletedGames },
            { PermissionManageEntities, PermissionValueManageEntities },
            { PermissionEditOrders, PermissionValueEditOrders },
            { PermissionOrderHistory, PermissionValueOrderHistory },
            { PermissionOrderStatus, PermissionValueOrderStatus },
            { PermissionBanUsers, PermissionValueBanUsers },
            { PermissionModerateComments, PermissionValueModerateComments },
        };
}
