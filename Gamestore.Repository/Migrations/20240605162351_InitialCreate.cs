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
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ParentGenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Platforms",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "GameGenres",
                columns: table => new
                {
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    PlatformId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                name: "GameGenres");

            migrationBuilder.DropTable(
                name: "GamePlatforms");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Platforms");

            migrationBuilder.DropTable(
                name: "Publishers");
        }
    }
}
