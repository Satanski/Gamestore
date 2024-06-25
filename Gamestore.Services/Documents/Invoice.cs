using Gamestore.DAL.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Gamestore.BLL.Documents;

public class Invoice(Order order, double amountToPay) : IDocument
{
    public void Compose(IDocumentContainer container)
    {
        var validTill = order.Date.AddDays(14);

        container
             .Page(page =>
             {
                 page.Size(PageSizes.A4);
                 page.Margin(2, Unit.Centimetre);
                 page.PageColor(Colors.White);
                 page.DefaultTextStyle(x => x.FontSize(12));

                 page.Header()
                     .Text("Invoice")
                     .SemiBold().FontSize(36).FontColor(Colors.Blue.Medium)
                     .AlignCenter();

                 page.Content()
                     .PaddingVertical(1, Unit.Centimetre)
                     .Column(column =>
                     {
                         column.Item().Text($"Customer: {order.CustomerId}");
                         column.Item().Text($"Order id: {order.Id}");
                         column.Item().Text($"Creation date: {order.Date}");
                         column.Item().Text($"Valid till: {validTill:dd-MM-yyyy}");
                         column.Item().Text($"Amount to pay: {amountToPay}");
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
