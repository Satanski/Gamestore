using Gamestore.BLL.Models.Payment;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Gamestore.BLL.Documents;

public class Invoice(PaymentModelDto payment) : IDocument
{
    public void Compose(IDocumentContainer container)
    {
        container
             .Page(page =>
             {
                 page.Size(PageSizes.A4);
                 page.Margin(2, Unit.Centimetre);
                 page.PageColor(Colors.White);
                 page.DefaultTextStyle(x => x.FontSize(20));

                 page.Header()
                     .Text("Invoice")
                     .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium)
                     .AlignCenter();

                 page.Content()
                     .PaddingVertical(1, Unit.Centimetre)
                     .Column(column =>
                     {
                         column.Item().Text($"User id: {payment.UserId}");
                         column.Item().Text($"Order id: {payment.UserId}");
                         column.Item().Text($"Creation date: {payment.UserId}");
                         column.Item().Text($"Valid till: {payment.UserId}");
                         column.Item().Text($"Product: {payment.Sum}");
                     });

                 page.Footer()
                     .AlignCenter()
                     .Text(x =>
                     {
                         x.Span("Page ");
                         x.CurrentPageNumber();
                         x.Span(" of ");
                         x.TotalPages();
                     });
             });
    }
}
