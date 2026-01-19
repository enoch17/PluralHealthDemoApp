namespace PluralHealthDemoApp.Exceptions
{
    public class AccessDeniedException(string message) : Exception(message ?? "An Access Denied error occurred.")
    {
    }
}
