public interface INotificationService
{
    void Send(string message);
}

public class NotificationService : INotificationService
{
    public void Send(string message)
    {
        Console.WriteLine($"[ALERT]: {message}");
    }
}