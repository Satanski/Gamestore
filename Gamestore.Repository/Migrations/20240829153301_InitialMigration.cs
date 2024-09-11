using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gamestore.DAL.Migrations;

/// <inheritdoc />
public partial class InitialMigration : Migration
{
    private static readonly string[] Columns = ["Id", "Name", "ParentGenreId"];
    private static readonly string[] ColumnsArray = ["Id", "Type"];
    private static readonly string[] ColumnsArray0 = ["Id", "Address", "City", "CompanyName", "ContactName", "ContactTitle", "Country", "Description", "Fax", "HomePage", "Phone", "PostalCode", "Region"];
    private static readonly string[] ColumnsArray1 = ["Id", "Description", "Discount", "IsDeleted", "Key", "Name", "NumberOfViews", "Price", "PublishDate", "PublisherId", "QuantityPerUnit", "ReorderLevel", "UnitInStock", "UnitsOnOrder"];

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Genres",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                ParentGenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Genres", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Orders",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Status = table.Column<int>(type: "int", nullable: false),
                OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                RequiredDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                ShippedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                EmployeeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                ShipVia = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                Freight = table.Column<float>(type: "real", nullable: true),
                ShipName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ShipAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ShipCity = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ShipRegion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ShipPostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ShipCountry = table.Column<string>(type: "nvarchar(max)", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Orders", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Platforms",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Type = table.Column<string>(type: "nvarchar(450)", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Platforms", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Publishers",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CompanyName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                HomePage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ContactTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Region = table.Column<string>(type: "nvarchar(max)", nullable: true),
                PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Publishers", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Games",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                Price = table.Column<double>(type: "float", nullable: false),
                UnitInStock = table.Column<int>(type: "int", nullable: false),
                Discount = table.Column<int>(type: "int", nullable: false),
                PublisherId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PublishDate = table.Column<DateOnly>(type: "date", nullable: false),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NumberOfViews = table.Column<int>(type: "int", nullable: false),
                ReorderLevel = table.Column<int>(type: "int", nullable: true),
                QuantityPerUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                UnitsOnOrder = table.Column<int>(type: "int", nullable: true),
                IsDeleted = table.Column<bool>(type: "bit", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Games", x => x.Id);
                table.ForeignKey(
                    name: "FK_Games_Publishers_PublisherId",
                    column: x => x.PublisherId,
                    principalTable: "Publishers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Comments",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ParentCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Comments", x => x.Id);
                table.ForeignKey(
                    name: "FK_Comments_Comments_ParentCommentId",
                    column: x => x.ParentCommentId,
                    principalTable: "Comments",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Comments_Games_GameId",
                    column: x => x.GameId,
                    principalTable: "Games",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "GameGenres",
            columns: table => new
            {
                GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GameGenres", x => new { x.GameId, x.GenreId });
                table.ForeignKey(
                    name: "FK_GameGenres_Games_GameId",
                    column: x => x.GameId,
                    principalTable: "Games",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_GameGenres_Genres_GenreId",
                    column: x => x.GenreId,
                    principalTable: "Genres",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "GamePlatforms",
            columns: table => new
            {
                GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                PlatformId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_GamePlatforms", x => new { x.GameId, x.PlatformId });
                table.ForeignKey(
                    name: "FK_GamePlatforms_Games_GameId",
                    column: x => x.GameId,
                    principalTable: "Games",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_GamePlatforms_Platforms_PlatformId",
                    column: x => x.PlatformId,
                    principalTable: "Platforms",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "OrderGames",
            columns: table => new
            {
                OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Price = table.Column<double>(type: "float", nullable: false),
                Quantity = table.Column<int>(type: "int", nullable: false),
                Discount = table.Column<int>(type: "int", nullable: true),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_OrderGames", x => new { x.OrderId, x.GameId });
                table.ForeignKey(
                    name: "FK_OrderGames_Games_GameId",
                    column: x => x.GameId,
                    principalTable: "Games",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_OrderGames_Orders_OrderId",
                    column: x => x.OrderId,
                    principalTable: "Orders",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.InsertData(
            table: "Genres",
            columns: Columns,
            values: new object[,]
            {
                { new Guid("0abae6f9-3329-405d-9e01-a372ec794962"), "RPG", null },
                { new Guid("0cf312ae-1773-4a84-89d3-5c117201f7b5"), "Puzzle & Skill", null },
                { new Guid("2e50ac36-269b-4b31-924b-55535cba03b7"), "Rally", new Guid("8a767f88-e79e-4340-a6de-f363b82232f7") },
                { new Guid("3dc5a59e-0bfa-498a-899c-25b91266a680"), "Strategy", null },
                { new Guid("420a447b-c138-41a6-81d4-37e11490f270"), "TPS", new Guid("802bcf55-b599-4877-802e-d1d80055fdca") },
                { new Guid("4268dd68-ac12-4c76-98d1-757e8bbc96fe"), "Off-road", new Guid("8a767f88-e79e-4340-a6de-f363b82232f7") },
                { new Guid("496d7e78-9326-44bc-b3a3-b1bcd6cefb6f"), "RTS", new Guid("3dc5a59e-0bfa-498a-899c-25b91266a680") },
                { new Guid("4ef7e78e-edac-4ff9-baad-53d48e198318"), "Arcade", new Guid("8a767f88-e79e-4340-a6de-f363b82232f7") },
                { new Guid("6c8a9150-a1bd-424f-ac06-c187b2105313"), "TBS", new Guid("3dc5a59e-0bfa-498a-899c-25b91266a680") },
                { new Guid("802bcf55-b599-4877-802e-d1d80055fdca"), "Action", null },
                { new Guid("8a767f88-e79e-4340-a6de-f363b82232f7"), "Races", null },
                { new Guid("8c7d99fb-9f66-4797-a8c1-8cb26c1f80e2"), "Sports", null },
                { new Guid("9a3f8bbb-2522-4809-8c89-2f1fa00d5b13"), "FPS", new Guid("802bcf55-b599-4877-802e-d1d80055fdca") },
                { new Guid("b1fa0d7b-1ac1-4f9f-8fbb-e1702afe26c8"), "Formula", new Guid("8a767f88-e79e-4340-a6de-f363b82232f7") },
                { new Guid("c76a84b7-647e-4574-8a7e-adb3c3edb3d0"), "Adventure", null },
            });

        migrationBuilder.InsertData(
            table: "Platforms",
            columns: ColumnsArray,
            values: new object[,]
            {
                { new Guid("1ec1fc35-9be8-4d08-8a30-7d4068775846"), "Console" },
                { new Guid("674fd7f3-47d4-4aaf-bd89-798a2a9bbc4b"), "Desktop" },
                { new Guid("c6bb34c8-14dd-4227-9430-6bdee1934db0"), "Browser" },
                { new Guid("efdef1b8-8077-4faf-a4db-dc9102bee14b"), "Mobile" },
            });

        migrationBuilder.InsertData(
            table: "Publishers",
            columns: ColumnsArray0,
            values: new object[,]
            {
                { new Guid("11111111-1111-1111-1111-111111111111"), null, null, "Blizzard", null, null, null, null, null, "www.blizzard.com", null, null, null },
                { new Guid("22222222-2222-2222-2222-222222222222"), null, null, "Elecrtonic Arts", null, null, null, null, null, "www.ea.com", null, null, null },
            });

        migrationBuilder.InsertData(
            table: "Games",
            columns: ColumnsArray1,
            values: new object[,]
            {
                { new Guid("73ebc259-405c-4118-9542-afd14681a50e"), "Rpg game", 10, false, "BG", "Baldurs Gate", 0, 250.0, new DateOnly(1, 1, 1), new Guid("22222222-2222-2222-2222-222222222222"), null, null, 15, null },
                { new Guid("77bfd847-fe75-4bbb-9dce-46f6407ff992"), "Racing game", 0, false, "TD", "Tedt Drive", 0, 150.0, new DateOnly(1, 1, 1), new Guid("11111111-1111-1111-1111-111111111111"), null, null, 2, null },
            });

        migrationBuilder.CreateIndex(
            name: "IX_Comments_GameId",
            table: "Comments",
            column: "GameId");

        migrationBuilder.CreateIndex(
            name: "IX_Comments_ParentCommentId",
            table: "Comments",
            column: "ParentCommentId");

        migrationBuilder.CreateIndex(
            name: "IX_GameGenres_GenreId",
            table: "GameGenres",
            column: "GenreId");

        migrationBuilder.CreateIndex(
            name: "IX_GamePlatforms_PlatformId",
            table: "GamePlatforms",
            column: "PlatformId");

        migrationBuilder.CreateIndex(
            name: "IX_Games_Key",
            table: "Games",
            column: "Key",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Games_PublisherId",
            table: "Games",
            column: "PublisherId");

        migrationBuilder.CreateIndex(
            name: "IX_Genres_Name",
            table: "Genres",
            column: "Name",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_OrderGames_GameId",
            table: "OrderGames",
            column: "GameId");

        migrationBuilder.CreateIndex(
            name: "IX_Platforms_Type",
            table: "Platforms",
            column: "Type",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Publishers_CompanyName",
            table: "Publishers",
            column: "CompanyName",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Comments");

        migrationBuilder.DropTable(
            name: "GameGenres");

        migrationBuilder.DropTable(
            name: "GamePlatforms");

        migrationBuilder.DropTable(
            name: "OrderGames");

        migrationBuilder.DropTable(
            name: "Genres");

        migrationBuilder.DropTable(
            name: "Platforms");

        migrationBuilder.DropTable(
            name: "Games");

        migrationBuilder.DropTable(
            name: "Orders");

        migrationBuilder.DropTable(
            name: "Publishers");
    }
}
