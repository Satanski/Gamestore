namespace Gamestore.BLL.Models.Payment;

public record VisaPaymentModel
{
    public string Holder { get; set; }

    public string CardNumber { get; set; }

    public int MonthExpire { get; set; }

    public int YearExpire { get; set; }

    public int Cvv2 { get; set; }

    public double TransactionAmount { get; set; }
}
