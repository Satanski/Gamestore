using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
namespace Gamestore.Repository.Migrations;

/// <inheritdoc />
public partial class AddInitialSeed : Migration
{
    private static readonly string[] Columns = ["Id", "Type"];

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Games",
            columns: ["Id", "Description", "Key", "Name"],
            values: [new Guid("598cca88-5cf1-4d3f-982f-efec7b22d9fa"), "Desc", "Key", "Gra testowa nazwa"]);

        migrationBuilder.InsertData(
            table: "Genres",
            columns: ["Id", "Name", "ParentGenreId"],
            values: new object[,]
            {
                { new Guid("0bcb396a-4c5c-4d91-8dd9-314253170b3d"), "Sports", null },
                { new Guid("0bf5f3e7-30b3-452a-b858-aec7e9464e9f"), "Races", null },
                { new Guid("0e460506-fb1a-4884-a353-e92564075deb"), "Arcade", new Guid("0bf5f3e7-30b3-452a-b858-aec7e9464e9f") },
                { new Guid("2efaafb1-fe1f-4e96-88f2-d6bd2f96b722"), "Adventure", null },
                { new Guid("67954f5e-533d-4018-b843-9c8293fdb812"), "Action", null },
                { new Guid("689addc0-1f50-4709-b56f-15b4ea239301"), "Rally", new Guid("0bf5f3e7-30b3-452a-b858-aec7e9464e9f") },
                { new Guid("81632d54-df13-421d-8199-66df47a421c0"), "Puzzle & Skill", null },
                { new Guid("8c1da889-e9df-4c95-9a31-5cb92a6676ea"), "RPG", null },
                { new Guid("94122e07-cef9-4d44-bc46-d68994cf1645"), "FPS", new Guid("67954f5e-533d-4018-b843-9c8293fdb812") },
                { new Guid("9b7f5548-30e9-492a-87b6-a523aeea3d77"), "RTS", new Guid("bd73acbf-286e-4e2b-87c2-d93370cf0d8f") },
                { new Guid("a932d624-9f04-469f-bdf3-474925221ece"), "TBS", new Guid("bd73acbf-286e-4e2b-87c2-d93370cf0d8f") },
                { new Guid("b837e8b1-2ced-4401-8a07-9be1347e56d4"), "TPS", new Guid("67954f5e-533d-4018-b843-9c8293fdb812") },
                { new Guid("bd73acbf-286e-4e2b-87c2-d93370cf0d8f"), "Strategy", null },
                { new Guid("e218efa6-0f74-4598-8e5e-7a4ae4122ed1"), "Off-road", new Guid("0bf5f3e7-30b3-452a-b858-aec7e9464e9f") },
                { new Guid("e4fb844b-1ad4-45f0-8ec1-fa5f6a3bfcf7"), "Formula", new Guid("0bf5f3e7-30b3-452a-b858-aec7e9464e9f") },
            });

        migrationBuilder.InsertData(
            table: "Platforms",
            columns: Columns,
            values: new object[,]
            {
                { new Guid("09e1fcba-7058-4355-b133-f9e70560296b"), "Desktop" },
                { new Guid("290954a9-b253-431f-a5b9-c7a355388e80"), "Browser" },
                { new Guid("4480b8d2-4c1e-422c-b751-8780dac77a0f"), "Console" },
                { new Guid("4ff2d831-0a0a-4400-9b85-4c06dfec15cd"), "Mobile" },
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "Games",
            keyColumn: "Id",
            keyValue: new Guid("598cca88-5cf1-4d3f-982f-efec7b22d9fa"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("0bcb396a-4c5c-4d91-8dd9-314253170b3d"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("0bf5f3e7-30b3-452a-b858-aec7e9464e9f"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("0e460506-fb1a-4884-a353-e92564075deb"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("2efaafb1-fe1f-4e96-88f2-d6bd2f96b722"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("67954f5e-533d-4018-b843-9c8293fdb812"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("689addc0-1f50-4709-b56f-15b4ea239301"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("81632d54-df13-421d-8199-66df47a421c0"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("8c1da889-e9df-4c95-9a31-5cb92a6676ea"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("94122e07-cef9-4d44-bc46-d68994cf1645"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("9b7f5548-30e9-492a-87b6-a523aeea3d77"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("a932d624-9f04-469f-bdf3-474925221ece"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("b837e8b1-2ced-4401-8a07-9be1347e56d4"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("bd73acbf-286e-4e2b-87c2-d93370cf0d8f"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("e218efa6-0f74-4598-8e5e-7a4ae4122ed1"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("e4fb844b-1ad4-45f0-8ec1-fa5f6a3bfcf7"));

        migrationBuilder.DeleteData(
            table: "Platforms",
            keyColumn: "Id",
            keyValue: new Guid("09e1fcba-7058-4355-b133-f9e70560296b"));

        migrationBuilder.DeleteData(
            table: "Platforms",
            keyColumn: "Id",
            keyValue: new Guid("290954a9-b253-431f-a5b9-c7a355388e80"));

        migrationBuilder.DeleteData(
            table: "Platforms",
            keyColumn: "Id",
            keyValue: new Guid("4480b8d2-4c1e-422c-b751-8780dac77a0f"));

        migrationBuilder.DeleteData(
            table: "Platforms",
            keyColumn: "Id",
            keyValue: new Guid("4ff2d831-0a0a-4400-9b85-4c06dfec15cd"));
    }
}
