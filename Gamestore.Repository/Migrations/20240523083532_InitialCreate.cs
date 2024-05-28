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
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });

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
                table: "Games",
                columns: new[] { "Id", "Description", "Key", "Name" },
                values: new object[] { new Guid("c6c84790-2f43-4f7c-b6df-94c70491343d"), "Desc", "Key", "Gra testowa nazwa" });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "Name", "ParentGenreId" },
                values: new object[,]
                {
                    { new Guid("200c416a-538c-498e-a2a0-cdf4c74e142b"), "TPS", new Guid("77c9ab07-21df-481a-aa0b-28f4a166e354") },
                    { new Guid("2c4a4834-8d51-47c6-a5ac-5db740e1b5f8"), "Strategy", null },
                    { new Guid("6021ab9f-0e15-447f-8f62-c26e904b1de6"), "Races", null },
                    { new Guid("658e2c8b-d5f5-4e23-a4ff-deea222748dc"), "TBS", new Guid("2c4a4834-8d51-47c6-a5ac-5db740e1b5f8") },
                    { new Guid("6cc174bc-1725-4f54-bda8-b57389288b2a"), "RTS", new Guid("2c4a4834-8d51-47c6-a5ac-5db740e1b5f8") },
                    { new Guid("77c9ab07-21df-481a-aa0b-28f4a166e354"), "Action", null },
                    { new Guid("824ead0b-4e95-4d7b-b9b8-f47b009442b3"), "Rally", new Guid("6021ab9f-0e15-447f-8f62-c26e904b1de6") },
                    { new Guid("88de2753-24ea-475f-a59f-0608359d2b8b"), "FPS", new Guid("77c9ab07-21df-481a-aa0b-28f4a166e354") },
                    { new Guid("8b447f60-7cca-460a-bc6a-36e02015f46a"), "Puzzle & Skill", null },
                    { new Guid("934c60a7-2755-4c12-8420-6b1e017c0144"), "Sports", null },
                    { new Guid("a864da3e-2e9e-4c03-8eaa-ca6c5322b1c9"), "Adventure", null },
                    { new Guid("ac35a93d-e380-4cfc-bf6a-683180718c4f"), "RPG", null },
                    { new Guid("ba252c3a-ff3d-42a1-ba75-2b21e8690452"), "Formula", new Guid("6021ab9f-0e15-447f-8f62-c26e904b1de6") },
                    { new Guid("dbf1e07c-93f8-4a9e-9238-3e027f13b8b7"), "Off-road", new Guid("6021ab9f-0e15-447f-8f62-c26e904b1de6") },
                    { new Guid("e5c77a2a-40f1-4dfa-b847-7b5b2c15e52d"), "Arcade", new Guid("6021ab9f-0e15-447f-8f62-c26e904b1de6") }
                });

            migrationBuilder.InsertData(
                table: "Platforms",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { new Guid("644a99d9-dce9-4057-b94e-ee704c620b61"), "Console" },
                    { new Guid("a7289405-cd3c-48e5-b858-9ae4faa889d4"), "Mobile" },
                    { new Guid("d1099105-d805-49e4-b2c7-c1fcf4040a81"), "Desktop" },
                    { new Guid("f29416dc-e41a-4131-8e66-93ec11bc8e45"), "Browser" }
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
                name: "IX_Genres_Name",
                table: "Genres",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_Type",
                table: "Platforms",
                column: "Type",
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
        }
    }
}
