using Lab.Application.Common.Interfaces.Persistence;
using Microsoft.Extensions.Logging;

namespace Lab.Application.Services;

public class CleanupService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<CleanupService> _logger;

    public CleanupService(IUnitOfWork unitOfWork, ILogger<CleanupService> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task CleanupOldTickets()
    {
        var cutoffDate = DateTime.UtcNow.AddDays(-30);
        var oldTickets = await _unitOfWork.Tickets.FindAsync(t => t.ClosedAt != null && t.ClosedAt < cutoffDate);

        foreach (var ticket in oldTickets)
        {
            _logger.LogInformation($"[Hangfire] Eliminando ticket cerrado: {ticket.TicketId}");
            await _unitOfWork.Tickets.RemoveAsync(ticket);
        }
        
        await _unitOfWork.SaveChangesAsync();
        _logger.LogInformation("[Hangfire] Limpieza de tickets antiguos completada");
    }
}