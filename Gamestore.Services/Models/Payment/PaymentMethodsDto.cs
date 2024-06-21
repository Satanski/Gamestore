namespace Gamestore.BLL.Models.Payment;

public record PaymentMethodsDto
{
    public List<PaymentMethod> PaymentMethods { get; set; }
}
