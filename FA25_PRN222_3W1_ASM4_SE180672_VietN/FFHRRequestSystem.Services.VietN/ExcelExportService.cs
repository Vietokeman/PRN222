using ClosedXML.Excel;
using FFHRRequestSystem.Entitites.VietN.Models;

namespace FFHRRequestSystem.Services.VietN
{
    public class ExcelExportService
    {
        public byte[] ExportTicketsToExcel(List<TicketProcessingVietN> tickets)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Ticket Processing");

            // Define headers with styling
            var headers = new[]
            {
                "Processing Code", "Ticket Reference", "Processing Action", "Action Description",
                "Priority Level", "Status", "Overdue Days", "Escalation Level",
                "Is Auto Processed", "Processed By", "Resolved Note", "Created Date",
                "Modified Date", "Type Name"
            };

            // Style the header row
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = worksheet.Cell(1, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
                cell.Style.Fill.BackgroundColor = XLColor.FromHtml("#FFC107");
                cell.Style.Font.FontColor = XLColor.White;
                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            }

            // Add data rows
            int row = 2;
            foreach (var ticket in tickets)
            {
                worksheet.Cell(row, 1).Value = ticket.ProcessingCode;
                worksheet.Cell(row, 2).Value = ticket.TicketReference;
                worksheet.Cell(row, 3).Value = ticket.ProcessingAction;
                worksheet.Cell(row, 4).Value = ticket.ActionDescription;
                worksheet.Cell(row, 5).Value = ticket.PriorityLevel.ToString();
                worksheet.Cell(row, 6).Value = ticket.Status.ToString();
                worksheet.Cell(row, 7).Value = ticket.OverdueDays?.ToString() ?? "";
                worksheet.Cell(row, 8).Value = ticket.EscalationLevel?.ToString() ?? "";
                worksheet.Cell(row, 9).Value = ticket.IsAutoProcessed ? "Yes" : "No";
                worksheet.Cell(row, 10).Value = ticket.ProcessedBy;
                worksheet.Cell(row, 11).Value = ticket.ResolvedNote;
                worksheet.Cell(row, 12).Value = ticket.CreatedDate?.ToString("dd/MM/yyyy HH:mm:ss") ?? "";
                worksheet.Cell(row, 13).Value = ticket.ModifiedDate?.ToString("dd/MM/yyyy HH:mm:ss") ?? "";
                worksheet.Cell(row, 14).Value = ticket.ProcessingTypeVietN?.TypeName ?? "";

                // Add borders to data cells
                for (int col = 1; col <= headers.Length; col++)
                {
                    worksheet.Cell(row, col).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                row++;
            }

            // Auto-fit columns
            worksheet.Columns().AdjustToContents();

            // Set column widths to reasonable values
            foreach (var column in worksheet.ColumnsUsed())
            {
                if (column.Width > 50)
                    column.Width = 50;
                else if (column.Width < 15)
                    column.Width = 15;
            }

            // Save to memory stream
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
