using ClosedXML.Excel;
using Lab.Application.Common.Interfaces.Persistence;

namespace Lab11.Infrastructure.Services;

public class ExcelReportService
{
    private readonly IUnitOfWork _unitOfWork;

    public ExcelReportService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<string> ExportUsersWithRolesAsync()
    {
        var users = await _unitOfWork.Users.GetAllWithRolesAsync();
        var filePath = Path.Combine("C:\\Users\\ribey\\Documents", "usuarios_roles.xlsx");

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("UsuariosRoles");

        worksheet.Cell(1, 1).Value = "Usuario";
        worksheet.Cell(1, 2).Value = "Email";
        worksheet.Cell(1, 3).Value = "Roles";

        int row = 2;
        foreach (var user in users)
        {
            worksheet.Cell(row, 1).Value = user.Username;
            worksheet.Cell(row, 2).Value = user.Email;
            worksheet.Cell(row, 3).Value = string.Join(", ", user.UserRoles.Select(r => r.Role.RoleName));
            row++;
        }

        workbook.SaveAs(filePath);
        return filePath;
    }

    public async Task<string> ExportClosedTicketsAsync()
    {
        var tickets = await _unitOfWork.Tickets.FindAsync(t => t.ClosedAt != null);
        var filePath = Path.Combine("C:\\Users\\ribey\\Documents", "tickets_cerrados.xlsx");

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("TicketsCerrados");

        worksheet.Cell(1, 1).Value = "Título";
        worksheet.Cell(1, 2).Value = "Estado";
        worksheet.Cell(1, 3).Value = "Usuario";
        worksheet.Cell(1, 4).Value = "Fecha Cierre";

        int row = 2;
        foreach (var ticket in tickets)
        {
            worksheet.Cell(row, 1).Value = ticket.Title;
            worksheet.Cell(row, 2).Value = ticket.Status;
            worksheet.Cell(row, 3).Value = ticket.User?.Username;
            worksheet.Cell(row, 4).Value = ticket.ClosedAt?.ToString("yyyy-MM-dd HH:mm");
            row++;
        }

        workbook.SaveAs(filePath);
        return filePath;
    }
}