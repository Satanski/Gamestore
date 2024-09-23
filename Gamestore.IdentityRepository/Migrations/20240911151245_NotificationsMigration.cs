using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamestore.IdentityRepository.Migrations;

/// <inheritdoc />
public partial class NotificationsMigration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "UserNotificationMethods",
            columns: table => new
            {
                UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                NotificationType = table.Column<string>(type: "nvarchar(450)", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserNotificationMethods", x => new { x.UserId, x.NotificationType });
                table.ForeignKey(
                    name: "FK_UserNotificationMethods_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "UserNotificationMethods");
    }
}
