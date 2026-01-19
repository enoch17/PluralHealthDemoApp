namespace PluralHealthDemoApp.Exceptions
{
    public class RequestValidationException(string message) : Exception(message ?? "A validation error occurred.")
    {
    }
}
