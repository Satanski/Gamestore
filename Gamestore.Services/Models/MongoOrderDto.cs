namespace Gamestore.BLL.Models;

public record MongoOrderDto
{
    public int OrderId { get; set; }

    public int CustomerId { get; set; }

    public int EmployeeId { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime RequireDate { get; set; }

    public DateTime ShippedDate { get; set; }

    public int ShipVia { get; set; }

    public double Freight { get; set; }

    public string ShipName { get; set; }

    public string ShipAddress { get; set; }

    public string ShipCity { get; set; }

    public string? ShipRegion { get; set; }

    public int ShipPostalCode { get; set; }

    public string ShipCountry { get; set; }
}
