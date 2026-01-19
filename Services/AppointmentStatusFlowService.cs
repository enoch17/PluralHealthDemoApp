using PluralHealthDemoApp.Exceptions;
using PluralHealthDemoApp.Models;

namespace PluralHealthDemoApp.Services
{
    public class AppointmentStatusFlowService
    {
        private readonly ILogger<AppointmentStatusFlowService> _logger;
        private readonly UserContext _userContext;

        public AppointmentStatusFlowService(
            UserContext userContext,
            ILogger<AppointmentStatusFlowService> logger)
        {
            _userContext = userContext;
            _logger = logger;
        }

        public void MoveToAwaitingVitals(Appointment appointment, string reason)
        {
            if (_userContext.Role == UserRole.Staff)
                throw new AccessDeniedException("You are not allowed to update patient status.");

            if (appointment.Clinic != _userContext.Clinic)
                throw new AccessDeniedException("Appointment belongs to another clinic.");

            if (appointment.AppointmentStatus == AppointmentStatus.AwaitingVitals)
                return; 

            appointment.AppointmentStatus = AppointmentStatus.AwaitingVitals;

            _logger.LogInformation(
                "Patient moved to AwaitingVitals. PatientCode={PatientCode},AppointmentId={AppointmentId}, Reason={Reason}",
                appointment.PatientCode,appointment.Id, reason);
        }
    }
}
