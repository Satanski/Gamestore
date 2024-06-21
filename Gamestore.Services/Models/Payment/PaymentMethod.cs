namespace Gamestore.BLL.Models.Payment;

public record PaymentMethod
{
    public string Title { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }
}
