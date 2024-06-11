#pragma warning disable IDE0079
#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional
#pragma warning disable CA1861
#pragma warning disable IDE0005
#pragma warning disable IDE0161
#pragma warning disable IDE0300
#pragma warning disable SA1413
#pragma warning disable SA1507
using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Gamestore.DAL.Migrations
{
    /// <inheritdoc />
    public partial class RealEpic3Update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("288c81b8-55c9-4e37-8ce1-dd7d76772277"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2a0fd9d4-6fc0-4732-a183-7e4d5db9f404"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2f1f7c5c-3760-4f85-9914-68d293f8ea95"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("49fab0b0-d4a7-4f97-ab04-9377116ca738"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("5925dc88-e01a-4a97-ad80-d1d2b8df8eb4"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("5ea9cfcb-895d-43cc-840a-78c61fbb145f"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("601a925c-e8d2-4cfa-a592-87eb5fab56ac"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("78934809-636e-4911-8caa-19837ce00b84"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("7a0da694-4c7c-48ad-bb26-d60f0cfef858"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("7e022c93-b14f-40c8-b85e-618beacb2752"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("824c13f4-050e-439f-abae-edc097e40407"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("8c2e8bb3-1710-481f-a330-8a4ebc6d04f7"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("92f488f7-357b-4eca-b651-985a03210c05"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("aa8d86f0-66dd-4d3a-9ba2-2c57d9761c36"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("d716dfad-336e-4298-81d0-6742bd4c6bba"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("23ade31d-a9d4-4d71-8044-4378d14d35e4"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("765c73bb-1c56-42dc-b92f-3357fac07bf6"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("b762fd5c-a5c2-475c-a11c-8f178bd4bd2c"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("ebaa5feb-21f5-4abc-8c27-91f2ed8ae929"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("3bdf3c48-f33a-47ed-a4f0-1362e873892b"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("b3049685-ae4e-4e49-8cce-3e75dce4ca16"));

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CustomerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderGames",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: true),
                    OrderId1 = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGames", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderGames_Games_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderGames_Orders_OrderId1",
                        column: x => x.OrderId1,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { new Guid("0a267dfc-6a87-4772-9959-8861a140d7ff"), "Strategy", null },
                    { new Guid("2a9e8784-d600-4a06-a5a0-4b46318985ad"), "TBS", new Guid("0a267dfc-6a87-4772-9959-8861a140d7ff") },
                    { new Guid("37e7af05-43a2-46a2-adf5-6221d10b735e"), "Adventure", null },
                    { new Guid("3b135dad-a00d-46ec-897e-5a83dfbf2639"), "TPS", new Guid("979316ee-c607-44fe-b884-c3732b1668ba") },
                    { new Guid("3f65ebdb-c974-4f14-8150-b33f33c93f08"), "Puzzle & Skill", null },
                    { new Guid("4e5f2bf4-9ecc-4ed8-b955-0def17e7dfe9"), "Sports", null },
                    { new Guid("56f7fbef-18f6-4c5d-bde4-a45a1947b5c6"), "Rally", new Guid("6be1be5f-57b2-4f84-aa8e-2b3f7015a0cd") },
                    { new Guid("66a826b7-8e8b-40b7-b279-dd9ba0ee9416"), "Arcade", new Guid("6be1be5f-57b2-4f84-aa8e-2b3f7015a0cd") },
                    { new Guid("688001a0-5f22-43f2-95c4-2022570f7b94"), "RPG", null },
                    { new Guid("6be1be5f-57b2-4f84-aa8e-2b3f7015a0cd"), "Races", null },
                    { new Guid("979316ee-c607-44fe-b884-c3732b1668ba"), "Action", null },
                    { new Guid("aef9be7a-45fb-444f-bae7-8d7fe228b3b1"), "FPS", new Guid("979316ee-c607-44fe-b884-c3732b1668ba") },
                    { new Guid("b105cfc2-110c-45a4-b4b1-c0ae49bf3e2d"), "RTS", new Guid("0a267dfc-6a87-4772-9959-8861a140d7ff") },
                    { new Guid("df0aca19-758c-414f-8908-3d4a51c4f3a2"), "Off-road", new Guid("6be1be5f-57b2-4f84-aa8e-2b3f7015a0cd") },
                    { new Guid("ee2fd1ac-d767-4ca5-9303-eb2a2f4a0a44"), "Formula", new Guid("6be1be5f-57b2-4f84-aa8e-2b3f7015a0cd") }
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("4fa0039a-076d-486d-9f83-c0d20670c01e"), "Console" },
                    { new Guid("613cd41c-757d-49eb-a3c2-ac11e24e46ed"), "Desktop" },
                    { new Guid("b7694479-4928-4116-ac8b-fd9cbdd8c190"), "Mobile" },
                    { new Guid("dab23934-42f3-4384-a633-547726737736"), "Browser" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CompanyName", "Description", "HomePage" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Blizzard", null, "www.blizzard.com" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Elecrtonic Arts", null, "www.ea.com" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Description", "Discount", "Key", "Name", "Price", "PublisherId", "UnitInStock" },
                values: new object[,]
                {
                    { new Guid("081bc243-3020-44aa-92ae-772ed63f1447"), "Racing game", 0, "TD", "Tedt Drive", 150.0, new Guid("11111111-1111-1111-1111-111111111111"), 2 },
                    { new Guid("15fcdae8-8f4a-4053-a92f-2dc3a010a1c2"), "Rpg game", 10, "BG", "Baldurs Gate", 250.0, new Guid("22222222-2222-2222-2222-222222222222"), 15 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderGames_OrderId_ProductId",
                table: "OrderGames",
                columns: new[] { "OrderId", "ProductId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderGames_OrderId1",
                table: "OrderGames",
                column: "OrderId1");

            migrationBuilder.CreateIndex(
                name: "IX_OrderGames_ProductId",
                table: "OrderGames",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderGames");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("081bc243-3020-44aa-92ae-772ed63f1447"));

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: new Guid("15fcdae8-8f4a-4053-a92f-2dc3a010a1c2"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("0a267dfc-6a87-4772-9959-8861a140d7ff"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("2a9e8784-d600-4a06-a5a0-4b46318985ad"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("37e7af05-43a2-46a2-adf5-6221d10b735e"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("3b135dad-a00d-46ec-897e-5a83dfbf2639"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("3f65ebdb-c974-4f14-8150-b33f33c93f08"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("4e5f2bf4-9ecc-4ed8-b955-0def17e7dfe9"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("56f7fbef-18f6-4c5d-bde4-a45a1947b5c6"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("66a826b7-8e8b-40b7-b279-dd9ba0ee9416"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("688001a0-5f22-43f2-95c4-2022570f7b94"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("6be1be5f-57b2-4f84-aa8e-2b3f7015a0cd"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("979316ee-c607-44fe-b884-c3732b1668ba"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("aef9be7a-45fb-444f-bae7-8d7fe228b3b1"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("b105cfc2-110c-45a4-b4b1-c0ae49bf3e2d"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("df0aca19-758c-414f-8908-3d4a51c4f3a2"));

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: new Guid("ee2fd1ac-d767-4ca5-9303-eb2a2f4a0a44"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("4fa0039a-076d-486d-9f83-c0d20670c01e"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("613cd41c-757d-49eb-a3c2-ac11e24e46ed"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("b7694479-4928-4116-ac8b-fd9cbdd8c190"));

            migrationBuilder.DeleteData(
                table: "Platforms",
                keyColumn: "Id",
                keyValue: new Guid("dab23934-42f3-4384-a633-547726737736"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Publishers",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { new Guid("288c81b8-55c9-4e37-8ce1-dd7d76772277"), "Action", null },
                    { new Guid("2a0fd9d4-6fc0-4732-a183-7e4d5db9f404"), "Off-road", new Guid("92f488f7-357b-4eca-b651-985a03210c05") },
                    { new Guid("2f1f7c5c-3760-4f85-9914-68d293f8ea95"), "Arcade", new Guid("92f488f7-357b-4eca-b651-985a03210c05") },
                    { new Guid("49fab0b0-d4a7-4f97-ab04-9377116ca738"), "FPS", new Guid("288c81b8-55c9-4e37-8ce1-dd7d76772277") },
                    { new Guid("5925dc88-e01a-4a97-ad80-d1d2b8df8eb4"), "Rally", new Guid("92f488f7-357b-4eca-b651-985a03210c05") },
                    { new Guid("5ea9cfcb-895d-43cc-840a-78c61fbb145f"), "Strategy", null },
                    { new Guid("601a925c-e8d2-4cfa-a592-87eb5fab56ac"), "Sports", null },
                    { new Guid("78934809-636e-4911-8caa-19837ce00b84"), "TBS", new Guid("5ea9cfcb-895d-43cc-840a-78c61fbb145f") },
                    { new Guid("7a0da694-4c7c-48ad-bb26-d60f0cfef858"), "RPG", null },
                    { new Guid("7e022c93-b14f-40c8-b85e-618beacb2752"), "Adventure", null },
                    { new Guid("824c13f4-050e-439f-abae-edc097e40407"), "Puzzle & Skill", null },
                    { new Guid("8c2e8bb3-1710-481f-a330-8a4ebc6d04f7"), "TPS", new Guid("288c81b8-55c9-4e37-8ce1-dd7d76772277") },
                    { new Guid("92f488f7-357b-4eca-b651-985a03210c05"), "Races", null },
                    { new Guid("aa8d86f0-66dd-4d3a-9ba2-2c57d9761c36"), "Formula", new Guid("92f488f7-357b-4eca-b651-985a03210c05") },
                    { new Guid("d716dfad-336e-4298-81d0-6742bd4c6bba"), "RTS", new Guid("5ea9cfcb-895d-43cc-840a-78c61fbb145f") }
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("23ade31d-a9d4-4d71-8044-4378d14d35e4"), "Console" },
                    { new Guid("765c73bb-1c56-42dc-b92f-3357fac07bf6"), "Browser" },
                    { new Guid("b762fd5c-a5c2-475c-a11c-8f178bd4bd2c"), "Desktop" },
                    { new Guid("ebaa5feb-21f5-4abc-8c27-91f2ed8ae929"), "Mobile" }
                });

            migrationBuilder.InsertData(
                table: "Publishers",
                columns: new[] { "Id", "CompanyName", "Description", "HomePage" },
                values: new object[,]
                {
                    { new Guid("3bdf3c48-f33a-47ed-a4f0-1362e873892b"), "Elecrtonic Arts", null, "www.ea.com" },
                    { new Guid("b3049685-ae4e-4e49-8cce-3e75dce4ca16"), "Blizzard", null, "www.blizzard.com" }
                });
        }
    }
}
