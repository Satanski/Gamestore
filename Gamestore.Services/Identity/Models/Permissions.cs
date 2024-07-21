namespace Gamestore.BLL.Identity.Models;

public static class Permissions
{
    private static readonly Dictionary<string, string> PermissionListDictionary = new()
    {
        { "permissions.add.comment", "AddComment" },
        { "permissions.delete.comment", "DeleteComment" },
        { "permissions.buy.game", "BuyGame" },
        { "permissions.manage.users", "ManageUsers" },
        { "permissions.manage.roles", "ManageRoles" },
        { "permissions.deleted.games", "DeletedGames" },
        { "permissions.manage.entities", "ManageEntities" },
        { "permissions.edit.orders", "EditOrders" },
        { "permissions.order.history", "OrderHistory" },
        { "permissions.order.status", "OrderStatus" },
        { "permissions.ban.users", "BanUsers" },
        { "permissions.moderate.comments", "ModerateComments" },
    };

    public static Dictionary<string, string> PermissionList => PermissionListDictionary;
}
