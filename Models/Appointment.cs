namespace PluralHealthDemoApp.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public required string PatientCode { get; set; }
        public AppointmentStatus AppointmentStatus { get; set; } = AppointmentStatus.Open;
        public required string Clinic {  get; set; } 
        public Patient? Patient { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

    }
}
