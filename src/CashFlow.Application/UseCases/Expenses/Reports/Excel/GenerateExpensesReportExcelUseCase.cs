using CashFlow.Application.UseCases.Expenses.Reports.Excel;
using CashFlow.Domain.Reports;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public class GenerateExpensesReportExcelUseCase : IGenerateExpensesReportExcelUseCase
{
    public async Task<byte[]> Execute(DateOnly month)
    {
        var workBook = new XLWorkbook();

        workBook.Author = "Pedro Sodré Malini";
        workBook.Style.Font.FontSize = 12;
        workBook.Style.Font.FontName = "Times New Roman";

        var workSheet = workBook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(workSheet);

        var file = new MemoryStream();
        workBook.SaveAs(file);

        return file.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessages.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessages.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessages.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessages.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessages.DESCRIPTION;

        worksheet.Cell("A1:E1").Style.Font.Bold = true; // DE A1 ATÉ E1 É BOLD
        worksheet.Cell("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#F5C2B6"); // XLColor.Aqua seria azul agua; // DE A1 ATÉ E1 É ROSINHA

        worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

    }
}