using Lab11.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportesController : ControllerBase
{
    private readonly ExcelReportService _reportService;

    public ReportesController(ExcelReportService reportService)
    {
        _reportService = reportService;
    }

    [HttpGet("usuarios-roles")]
    public async Task<IActionResult> GenerarReporteUsuarios()
    {
        var ruta = await _reportService.ExportUsersWithRolesAsync();
        return Ok(new { mensaje = "Reporte generado", ruta });
    }

    [HttpGet("tickets-cerrados")]
    public async Task<IActionResult> GenerarReporteTickets()
    {
        var ruta = await _reportService.ExportClosedTicketsAsync();
        return Ok(new { mensaje = "Reporte generado", ruta });
    }
}