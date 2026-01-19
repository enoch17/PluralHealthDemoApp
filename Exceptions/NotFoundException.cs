namespace PluralHealthDemoApp.Exceptions
{
    public class NotFoundException(string message) : Exception(message?? "A Not Found error occurred.")
    {
    }
}
