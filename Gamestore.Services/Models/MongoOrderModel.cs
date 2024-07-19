using Gamestore.DAL.Enums;

namespace Gamestore.BLL.Models;

public class MongoOrderModel
{
    public Guid Id { get; set; }

    public string CustomerId { get; set; }

    public OrderStatus Status { get; set; }

    public DateTime Date { get; set; }

    public List<OrderGameModelDto> OrderGames { get; set; }
}
