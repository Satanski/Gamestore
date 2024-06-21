namespace Gamestore.BLL.Models.Payment;

public record IboxPaymentModel
{
    public Guid? AccountNumber { get; set; }

    public Guid? InvoiceNumber { get; set; }

    public decimal? TransactionAmount { get; set; }
}
