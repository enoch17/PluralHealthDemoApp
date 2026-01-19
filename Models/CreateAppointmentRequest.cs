using System.ComponentModel.DataAnnotations;

namespace PluralHealthDemoApp.Models
{
    public class CreateAppointmentRequest
    {
        public required string PatientCode { get; set; } 

        public required string Clinic { get; set; } 

        public required string Title { get; set; } 

        public DateTime AppointmentTime { get; set; }
    }
}
