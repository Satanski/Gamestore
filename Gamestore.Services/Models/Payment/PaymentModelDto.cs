namespace Gamestore.BLL.Models.Payment;

public record PaymentModelDto
{
    public string Method { get; set; }

    public VisaPaymentModel? Model { get; set; }

    public Guid? UserId { get; set; }

    public Guid? OrderId { get; set; }

    public DateOnly? PaymentDate { get; set; }

    public DateOnly? CreationDate { get; set; }

    public decimal? Sum { get; set; }
}
