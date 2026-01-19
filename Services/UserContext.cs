using PluralHealthDemoApp.Models;

namespace PluralHealthDemoApp.Services
{
    public class UserContext
    {
        public string Username { get; set; } = "demoUser";
        public UserRole Role { get; set; } = UserRole.FrontDesk;
        public string? Clinic { get; set; } = "Ikeja"; // null for Admin
    }
}
