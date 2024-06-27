namespace Gamestore.WebApi.Stubs;

public class CustomerStub
{
    public Guid Id { get; set; } = new Guid("3fa85f64-5717-4562-b3fc-2c963f66afa6");

    public static string Name { get; set; } = "Pawel";

    public static DateTime BannedTill { get; set; }
}
