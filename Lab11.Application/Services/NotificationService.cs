namespace Lab.Application.Services;

public class NotificationService
{
    public void SendNotification(string user)
    {
        Console.WriteLine($"[INICIO] Enviando notificación a {user} - {DateTime.Now}");

        // Simular error
        throw new Exception("Simulación de error: falló el envío de la notificación");

        // Esto nunca se ejecutará
        // Console.WriteLine($"[FIN] Notificación enviada a {user} - {DateTime.Now}");
    }
}