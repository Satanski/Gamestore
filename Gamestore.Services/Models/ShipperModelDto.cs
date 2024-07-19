namespace Gamestore.BLL.Models;

public record ShipperModelDto
{
    public int ShipperID { get; set; }

    public string CompanyName { get; set; }

    public string Phone { get; set; }
}
