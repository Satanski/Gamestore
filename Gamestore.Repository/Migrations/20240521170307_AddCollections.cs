using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamestore.Repository.Migrations;

/// <inheritdoc />
public partial class AddCollections : Migration
{
    private static readonly string[] Columns = ["Id", "Type"];
    private static readonly string[] ColumnsArray = ["Id", "Description", "Key", "Name"];
    private static readonly string[] ColumnsArray0 = ["Id", "Name", "ParentGenreId"];

    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
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

        migrationBuilder.InsertData(
            table: "Games",
            columns: ["Id", "Description", "Key", "Name"],
            values: [new Guid("5af69f88-027b-49cf-9089-22ce337cda46"), "Desc", "Key", "Gra testowa nazwa"]);

        migrationBuilder.InsertData(
            table: "Genres",
            columns: ["Id", "Name", "ParentGenreId"],
            values: new object[,]
            {
                { new Guid("0586a906-7f6f-4e5a-adc3-3cc60f22dfde"), "Formula", new Guid("c09db00d-3154-4f3b-b8ad-ee4a44265ff5") },
                { new Guid("3299d55b-e103-4edc-a12d-7aa66398325f"), "TPS", new Guid("77adb740-d9e0-4b28-b2ba-b9f52fb5bbc1") },
                { new Guid("47539675-2761-492c-b235-18a116e9892f"), "Strategy", null },
                { new Guid("5bd20d62-a61c-4692-8507-3b6b35458260"), "Puzzle & Skill", null },
                { new Guid("61939340-09c4-4a30-8b2b-da1e3551ca63"), "RPG", null },
                { new Guid("75887ad2-bd5e-4ada-bf08-577e0e863364"), "Off-road", new Guid("c09db00d-3154-4f3b-b8ad-ee4a44265ff5") },
                { new Guid("77adb740-d9e0-4b28-b2ba-b9f52fb5bbc1"), "Action", null },
                { new Guid("835db15b-efe3-46b2-9458-56e8d2da95a9"), "Rally", new Guid("c09db00d-3154-4f3b-b8ad-ee4a44265ff5") },
                { new Guid("a57b35e2-339b-43c9-9cea-cf265ee62c21"), "FPS", new Guid("77adb740-d9e0-4b28-b2ba-b9f52fb5bbc1") },
                { new Guid("bb346703-d68d-4518-b02f-ec0ab1f817ed"), "RTS", new Guid("47539675-2761-492c-b235-18a116e9892f") },
                { new Guid("bda9686d-3c0d-4dad-8965-68b2b0333d9c"), "Arcade", new Guid("c09db00d-3154-4f3b-b8ad-ee4a44265ff5") },
                { new Guid("c09db00d-3154-4f3b-b8ad-ee4a44265ff5"), "Races", null },
                { new Guid("c2674526-b091-4248-9f53-98363a4523a2"), "TBS", new Guid("47539675-2761-492c-b235-18a116e9892f") },
                { new Guid("d2e5ae88-1cc2-4db9-9aaa-8de456a5bc11"), "Adventure", null },
                { new Guid("de9e104c-08df-4279-b468-8de90cbe3819"), "Sports", null },
            });

        migrationBuilder.InsertData(
            table: "Platforms",
            columns: Columns,
            values: new object[,]
            {
                { new Guid("0782dd4e-b1e7-41ea-a5e2-ad2cef0b54d9"), "Console" },
                { new Guid("17f23b51-ce66-4e62-aac7-6c5ed33400d5"), "Mobile" },
                { new Guid("5ebe150d-5c5a-4587-8eb7-7e222e3e55fe"), "Browser" },
                { new Guid("75890db0-cd9a-4207-8789-4fc0290291ba"), "Desktop" },
            });

        migrationBuilder.AddForeignKey(
            name: "FK_GameGenres_Games_GameId",
            table: "GameGenres",
            column: "GameId",
            principalTable: "Games",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);

        migrationBuilder.AddForeignKey(
            name: "FK_GamePlatforms_Games_GameId",
            table: "GamePlatforms",
            column: "GameId",
            principalTable: "Games",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_GameGenres_Games_GameId",
            table: "GameGenres");

        migrationBuilder.DropForeignKey(
            name: "FK_GamePlatforms_Games_GameId",
            table: "GamePlatforms");

        migrationBuilder.DeleteData(
            table: "Games",
            keyColumn: "Id",
            keyValue: new Guid("5af69f88-027b-49cf-9089-22ce337cda46"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("0586a906-7f6f-4e5a-adc3-3cc60f22dfde"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("3299d55b-e103-4edc-a12d-7aa66398325f"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("47539675-2761-492c-b235-18a116e9892f"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("5bd20d62-a61c-4692-8507-3b6b35458260"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("61939340-09c4-4a30-8b2b-da1e3551ca63"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("75887ad2-bd5e-4ada-bf08-577e0e863364"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("77adb740-d9e0-4b28-b2ba-b9f52fb5bbc1"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("835db15b-efe3-46b2-9458-56e8d2da95a9"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("a57b35e2-339b-43c9-9cea-cf265ee62c21"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("bb346703-d68d-4518-b02f-ec0ab1f817ed"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("bda9686d-3c0d-4dad-8965-68b2b0333d9c"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("c09db00d-3154-4f3b-b8ad-ee4a44265ff5"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("c2674526-b091-4248-9f53-98363a4523a2"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("d2e5ae88-1cc2-4db9-9aaa-8de456a5bc11"));

        migrationBuilder.DeleteData(
            table: "Genres",
            keyColumn: "Id",
            keyValue: new Guid("de9e104c-08df-4279-b468-8de90cbe3819"));

        migrationBuilder.DeleteData(
            table: "Platforms",
            keyColumn: "Id",
            keyValue: new Guid("0782dd4e-b1e7-41ea-a5e2-ad2cef0b54d9"));

        migrationBuilder.DeleteData(
            table: "Platforms",
            keyColumn: "Id",
            keyValue: new Guid("17f23b51-ce66-4e62-aac7-6c5ed33400d5"));

        migrationBuilder.DeleteData(
            table: "Platforms",
            keyColumn: "Id",
            keyValue: new Guid("5ebe150d-5c5a-4587-8eb7-7e222e3e55fe"));

        migrationBuilder.DeleteData(
            table: "Platforms",
            keyColumn: "Id",
            keyValue: new Guid("75890db0-cd9a-4207-8789-4fc0290291ba"));

        migrationBuilder.InsertData(
            table: "Games",
            columns: ColumnsArray,
            values: [new Guid("598cca88-5cf1-4d3f-982f-efec7b22d9fa"), "Desc", "Key", "Gra testowa nazwa"]);

        migrationBuilder.InsertData(
            table: "Genres",
            columns: ColumnsArray0,
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
            columns: ["Id", "Type"],
            values: new object[,]
            {
                { new Guid("09e1fcba-7058-4355-b133-f9e70560296b"), "Desktop" },
                { new Guid("290954a9-b253-431f-a5b9-c7a355388e80"), "Browser" },
                { new Guid("4480b8d2-4c1e-422c-b751-8780dac77a0f"), "Console" },
                { new Guid("4ff2d831-0a0a-4400-9b85-4c06dfec15cd"), "Mobile" },
            });
    }
}
