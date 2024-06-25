namespace Gamestore.BLL.Models.Payment;

public record PaymentModelDto
{
    public string Method { get; set; }

    public VisaPaymentModel? Model { get; set; }
}
