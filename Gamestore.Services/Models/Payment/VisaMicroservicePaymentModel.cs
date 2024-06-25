namespace Gamestore.BLL.Models.Payment;

internal record VisaMicroservicePaymentModel
{
    public string CardHolderName { get; set; }

    public string CardNumber { get; set; }

    public int ExpirationMonth { get; set; }

    public int ExpirationYear { get; set; }

    public int Cvv { get; set; }

    public double TransactionAmount { get; set; }
}
