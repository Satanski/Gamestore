namespace Gamestore.BLL.Configurations;

public class PaymentServiceConfiguration
{
    public string VisaServiceUrl { get; } = "http://localhost:5000/api/payments/visa";

    public string IboxServiceUrl { get; } = "http://localhost:5000/api/payments/ibox";
}
