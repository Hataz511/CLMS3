public class AuditService
{
    public void Log(string action)
    {
        Console.WriteLine($"[AUDIT]: {action}");
    }
}