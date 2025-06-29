using Hangfire;
using Lab.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lab11.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    [HttpPost("fire-and-forget")]
    public IActionResult SendNow([FromBody] string user)
    {
        BackgroundJob.Enqueue<NotificationService>(service => service.SendNotification(user));
        return Ok("Notificación encolada (fire-and-forget).");
    }
    [HttpPost("delayed")]
    public IActionResult SendDelayed([FromBody] string user)
    {
        BackgroundJob.Schedule<NotificationService>(
            service => service.SendNotification(user),
            TimeSpan.FromSeconds(30)); // Se ejecutará después de 30 segundos

        return Ok("Notificación programada (delayed job).");
    }
}