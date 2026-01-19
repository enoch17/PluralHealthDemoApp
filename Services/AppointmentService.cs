using Microsoft.Extensions.Logging;
using PluralHealthDemoApp.Exceptions;
using PluralHealthDemoApp.Models;
using Serilog.Core;

namespace PluralHealthDemoApp.Services
{
    public class AppointmentService
    {
        private readonly ILogger<AppointmentService> _logger;
        private readonly UserContext _userContext;

        private static List<Appointment> Appointments { get; }
        static int nextId = 44;

        static AppointmentService()
        {
            Appointments =
              [
            new Appointment { Id = 1,  Title = "Teeth Cleaning", Description = "Routine dental cleaning", Time = new DateTime(2026, 1, 3,  9,  0, 0), PatientCode = "P001", Clinic = "Ikeja" },
            new Appointment { Id = 2,  Title = "Dental Checkup", Description = "General examination", Time = new DateTime(2026, 1, 3, 10,  0, 0), PatientCode = "P003", Clinic = "Ikeja" },
            new Appointment { Id = 3,  Title = "Cavity Filling", Description = "Tooth cavity filling", Time = new DateTime(2026, 1, 4, 11,  0, 0), PatientCode = "P005", Clinic = "Ikeja", AppointmentStatus = AppointmentStatus.Closed },
            new Appointment { Id = 4,  Title = "Root Canal", Description = "Root canal treatment", Time = new DateTime(2026, 1, 4, 13,  0, 0), PatientCode = "P002", Clinic = "Ikeja" },
            new Appointment { Id = 5,  Title = "Teeth Whitening", Description = "Whitening procedure", Time = new DateTime(2026, 1, 5,  9, 30, 0), PatientCode = "P004", Clinic = "Ikeja" },
            new Appointment { Id = 6,  Title = "Dental X-Ray", Description = "Dental imaging", Time = new DateTime(2026, 1, 6, 10, 30, 0), PatientCode = "P001", Clinic = "Ikeja", AppointmentStatus = AppointmentStatus.Closed },
            new Appointment { Id = 7,  Title = "Crown Placement", Description = "Dental crown", Time = new DateTime(2026, 1, 6, 12,  0, 0), PatientCode = "P002", Clinic = "Ikeja" },
            new Appointment { Id = 8,  Title = "Bridge Fitting", Description = "Dental bridge", Time = new DateTime(2026, 1, 7, 14,  0, 0), PatientCode = "P003", Clinic = "Ikeja" },
            new Appointment { Id = 9,  Title = "Wisdom Tooth Review", Description = "Wisdom tooth check", Time = new DateTime(2026, 1, 8, 15, 30, 0), PatientCode = "P004", Clinic = "Ikeja" },
            new Appointment { Id = 10, Title = "Fluoride Treatment", Description = "Fluoride application", Time = new DateTime(2026, 1, 9, 10,  0, 0), PatientCode = "P005", Clinic = "Ikeja", AppointmentStatus = AppointmentStatus.Closed },
            new Appointment { Id = 11, Title = "Dental Polishing", Description = "Teeth polishing", Time = new DateTime(2026, 1,10, 11,  0, 0), PatientCode = "P001", Clinic = "Ikeja" },
            new Appointment { Id = 12, Title = "Follow-up Visit", Description = "Post treatment", Time = new DateTime(2026, 1,10, 13,  0, 0), PatientCode = "P003", Clinic = "Ikeja" },
            new Appointment { Id = 13, Title = "Implant Consultation", Description = "Implant review", Time = new DateTime(2026, 1,11, 14,  0, 0), PatientCode = "P002", Clinic = "Ikeja" },
            new Appointment { Id = 14, Title = "Mouth Guard Fitting", Description = "Custom guard", Time = new DateTime(2026, 1,12, 15, 30, 0), PatientCode = "P004", Clinic = "Ikeja" },
            new Appointment { Id = 15, Title = "Sensitive Teeth Care", Description = "Sensitivity treatment", Time = new DateTime(2026, 1,13,  9, 15, 0), PatientCode = "P005", Clinic = "Ikeja" },
            new Appointment { Id = 16, Title = "Oral Health Advice", Description = "Preventive care", Time = new DateTime(2026, 1,13, 11, 45, 0), PatientCode = "P001", Clinic = "Ikeja" },
            new Appointment { Id = 17, Title = "Emergency Visit", Description = "Urgent care", Time = new DateTime(2026, 1,14, 12, 30, 0), PatientCode = "P003", Clinic = "Ikeja" },
            new Appointment { Id = 18, Title = "Retainer Adjustment", Description = "Retainer check", Time = new DateTime(2026, 1,15, 14,  0, 0), PatientCode = "P002", Clinic = "Ikeja" },
            new Appointment { Id = 19, Title = "Gum Treatment", Description = "Gum disease care", Time = new DateTime(2026, 1,16, 15,  0, 0), PatientCode = "P004", Clinic = "Ikeja" },
            new Appointment { Id = 20, Title = "Braces Consultation", Description = "Orthodontics", Time = new DateTime(2026, 1,17, 10, 30, 0), PatientCode = "P005", Clinic = "Ikeja" },
            new Appointment { Id = 21, Title = "Dental Review", Description = "Routine review", Time = new DateTime(2026, 1,18, 11, 30, 0), PatientCode = "P001", Clinic = "Ikeja" },
            new Appointment { Id = 22, Title = "Final Checkup", Description = "Monthly check", Time = new DateTime(2026, 1,19, 13, 30, 0), PatientCode = "P002", Clinic = "Ikeja" },
            new Appointment { Id = 23, Title = "Teeth Cleaning", Description = "Routine cleaning", Time = new DateTime(2026, 1, 3,  9,  0, 0), PatientCode = "P002", Clinic = "Lekki" },
            new Appointment { Id = 24, Title = "Dental Checkup", Description = "General check", Time = new DateTime(2026, 1, 3, 10, 30, 0), PatientCode = "P004", Clinic = "Lekki" },
            new Appointment { Id = 25, Title = "Tooth Extraction", Description = "Extraction", Time = new DateTime(2026, 1, 4, 11, 30, 0), PatientCode = "P001", Clinic = "Lekki" },
            new Appointment { Id = 26, Title = "Gum Treatment", Description = "Gum care", Time = new DateTime(2026, 1, 5, 12, 30, 0), PatientCode = "P003", Clinic = "Lekki" },
            new Appointment { Id = 27, Title = "Root Canal", Description = "Canal therapy", Time = new DateTime(2026, 1, 6, 13,  0, 0), PatientCode = "P005", Clinic = "Lekki" },
            new Appointment { Id = 28, Title = "Dental X-Ray", Description = "X-ray imaging", Time = new DateTime(2026, 1, 7, 14,  0, 0), PatientCode = "P002", Clinic = "Lekki" },
            new Appointment { Id = 29, Title = "Crown Placement", Description = "Crown fix", Time = new DateTime(2026, 1, 8, 15,  0, 0), PatientCode = "P004", Clinic = "Lekki" },
            new Appointment { Id = 30, Title = "Fluoride Treatment", Description = "Fluoride care", Time = new DateTime(2026, 1, 9, 10, 30, 0), PatientCode = "P001", Clinic = "Lekki" },
            new Appointment { Id = 31, Title = "Dental Polishing", Description = "Polish teeth", Time = new DateTime(2026, 1,10, 11, 30, 0), PatientCode = "P003", Clinic = "Lekki" },
            new Appointment { Id = 32, Title = "Follow-up Visit", Description = "Follow up", Time = new DateTime(2026, 1,11, 12, 30, 0), PatientCode = "P005", Clinic = "Lekki" },
            new Appointment { Id = 33, Title = "Implant Consultation", Description = "Implant review", Time = new DateTime(2026, 1,12, 13, 30, 0), PatientCode = "P002", Clinic = "Lekki" },
            new Appointment { Id = 34, Title = "Mouth Guard Fitting", Description = "Guard fitting", Time = new DateTime(2026, 1,13, 14, 30, 0), PatientCode = "P004", Clinic = "Lekki" },
            new Appointment { Id = 35, Title = "Sensitive Teeth Care", Description = "Sensitivity care", Time = new DateTime(2026, 1,14,  9, 30, 0), PatientCode = "P001", Clinic = "Lekki" },
            new Appointment { Id = 36, Title = "Oral Health Advice", Description = "Advice session", Time = new DateTime(2026, 1,15, 10, 30, 0), PatientCode = "P003", Clinic = "Lekki" },
            new Appointment { Id = 37, Title = "Emergency Visit", Description = "Emergency care", Time = new DateTime(2026, 1,16, 11, 30, 0), PatientCode = "P005", Clinic = "Lekki" },
            new Appointment { Id = 38, Title = "Retainer Adjustment", Description = "Retainer fix", Time = new DateTime(2026, 1,17, 12, 30, 0), PatientCode = "P002", Clinic = "Lekki" },
            new Appointment { Id = 39, Title = "Wisdom Tooth Review", Description = "Wisdom review", Time = new DateTime(2026, 1,18, 13, 30, 0), PatientCode = "P004", Clinic = "Lekki" },
            new Appointment { Id = 40, Title = "Braces Consultation", Description = "Braces consult", Time = new DateTime(2026, 1,19, 14, 30, 0), PatientCode = "P001", Clinic = "Lekki" },
            new Appointment { Id = 41, Title = "Dental Review", Description = "Routine review", Time = new DateTime(2026, 1,20, 15, 30, 0), PatientCode = "P003", Clinic = "Lekki" },
            new Appointment { Id = 42, Title = "Final Checkup", Description = "Final review", Time = new DateTime(2026, 1,21, 16,  0, 0), PatientCode = "P005", Clinic = "Lekki" },
            new Appointment { Id = 43, Title = "Preventive Care", Description = "Preventive check", Time = new DateTime(2026, 1,22, 10,  0, 0), PatientCode = "P002", Clinic = "Lekki" },
            new Appointment { Id = 44, Title = "Monthly Review", Description = "Month-end review", Time = new DateTime(2026, 1,23, 11,  0, 0), PatientCode = "P004", Clinic = "Lekki" }
];

        }

        public AppointmentService(ILogger<AppointmentService> logger, UserContext userContext)
        {
            _logger = logger;
            _userContext = userContext;
        }

        public PagedResult<Appointment> GetAll(
            DateTime? startDate = null,
            DateTime? endDate = null,
            string? clinic = null,
            string? search = null,
            string? sort = "asc",
            int page = 1,
            int pageSize = 20)
        {
            try
            {
                //Role Check
                if (_userContext.Role != UserRole.FrontDesk && _userContext.Role != UserRole.Admin)
                {
                    _logger.LogWarning(
                        "Access denied. Role={Role}, Username={Username}",
                        _userContext.Role, _userContext.Username);

                    throw new AccessDeniedException("You do not have access to this resource.");
                }

                //Facility Check
                if (!string.IsNullOrWhiteSpace(clinic) &&
                    !string.Equals(_userContext.Clinic, clinic, StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogWarning(
                        "Access denied. Role={Role}, Username={Username}, UserClinic={UserClinic}, RequestedClinic={RequestedClinic}",
                        _userContext.Role,
                        _userContext.Username,
                        _userContext.Clinic,
                        clinic);

                    throw new AccessDeniedException("You do not have access to this clinic.");
                }

                _logger.LogInformation("Loading appointment list");


                //Validating Inputs

                var clinicValidation = AppointmentValidationService.ValidateClinic(clinic);
                if (!clinicValidation.IsValid)
                {
                    _logger.LogWarning(clinicValidation.ErrorMessage);
                    throw new RequestValidationException(clinicValidation.ErrorMessage ?? "Validation Failed");
                }

                var timeValidation = AppointmentValidationService.ValidateTimeWindow(startDate, endDate);
                if (!timeValidation.IsValid)
                {
                    _logger.LogWarning(timeValidation.ErrorMessage);
                    throw new RequestValidationException(timeValidation.ErrorMessage ?? "Validation Failed");
                }

                var from = startDate ?? DateTime.Today;
                var to = endDate ?? DateTime.Today.AddDays(1).AddTicks(-1);

                foreach (var appointment in Appointments)
                {
                    appointment.Patient = PatientService.Get(appointment.PatientCode);
                }

                var query = Appointments
                    .Where(a => a.Time >= from && a.Time <= to)
                    .AsQueryable();


                // Facility scoping
                if (_userContext.Role != UserRole.Admin)
                {
                    query = query.Where(a =>
                        a.Clinic.Equals(_userContext.Clinic, StringComparison.OrdinalIgnoreCase));
                }


                // Applying Filters
                if (!string.IsNullOrWhiteSpace(clinic))
                {
                    _logger.LogInformation("Filter applied: clinic = {Clinic}", clinic);
                    query = query.Where(a =>
                        a.Clinic.Equals(clinic, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(search))
                {
                    _logger.LogInformation("Search executed: term = {Search}", search);
                    search = search.ToLower();

                    query = query.Where(a =>
                        a.Patient != null &&
                        (
                            a.Patient.Name.ToLower().Contains(search) ||
                            a.Patient.Code.ToLower().Contains(search) ||
                            a.Patient.PhoneNo.Contains(search)
                        )
                    );
                }

                if (sort == "desc")
                {
                    query = query.OrderByDescending(a => a.Time);
                }
                else
                {
                    query = query.OrderBy(a => a.Time);
                }

                var totalCount = query.Count();

                page = page < 1 ? 1 : page;
                pageSize = pageSize < 1 ? 20 : pageSize;

                var items = query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .Select(a => new Appointment
                    {
                        Id = a.Id,
                        Time = a.Time,
                        Title = a.Title,
                        AppointmentStatus = a.AppointmentStatus,
                        Clinic = a.Clinic,

                        Patient = a.Patient,
                        PatientCode = a.PatientCode,
                    })
                    .ToList();

                _logger.LogInformation("Appointment list loaded successfully. TotalCount={TotalCount}, Page={Page}, PageSize={PageSize}", totalCount, page, pageSize);

                var message = "";
                if (items.Count == 0)
                    message = "No appointments found for the selected filters";

                return new PagedResult<Appointment>
                {
                    Message = message,
                    TotalCount = totalCount,
                    Page = page,
                    PageSize = pageSize,
                    Items = items
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading appointment list");
                throw;
            }

        }


        public Appointment Get(int id)
        {
            try
            {
                //Role Check
                if (_userContext.Role != UserRole.FrontDesk && _userContext.Role != UserRole.Admin)
                {
                    _logger.LogWarning(
                        "Access denied. Role={Role}, Username={Username}",
                        _userContext.Role, _userContext.Username);

                    throw new AccessDeniedException("You do not have access to this resource.");
                }

                var foundAppointment = Appointments.FirstOrDefault(p => p.Id == id);

                if (foundAppointment == null)
                {
                    _logger.LogWarning(
                        "Appointment not found. Id={AppointmentId}, Username={Username}",
                        id,
                        _userContext.Username);

                    throw new NotFoundException("Appointment not found.");
                }


                //Facility Check
                if (!string.IsNullOrWhiteSpace(foundAppointment.Clinic) &&
                    !string.Equals(_userContext.Clinic, foundAppointment.Clinic, StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogWarning(
                        "Access denied. Role={Role}, Username={Username}, UserClinic={UserClinic}, RequestedClinic={RequestedClinic}",
                        _userContext.Role,
                        _userContext.Username,
                        _userContext.Clinic,
                        foundAppointment.Clinic);

                    throw new AccessDeniedException("You do not have access to this clinic.");
                }
                return foundAppointment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting appointment");
                throw;
            }
        }

        public Appointment CreateAppointment(CreateAppointmentRequest request)
        {
            try
            {
                _logger.LogInformation(
                "Attempting to create appointment. Username={Username}, Role={Role}, Clinic={Clinic}",
                _userContext.Username,
                _userContext.Role,
                request.Clinic);

                var clinicValidation = AppointmentValidationService.ValidateClinic(request.Clinic);
                if (!clinicValidation.IsValid)
                {
                    _logger.LogWarning(clinicValidation.ErrorMessage);
                    throw new RequestValidationException(clinicValidation.ErrorMessage ?? "Validation Failed");
                }

                // Role check
                if (_userContext.Role != UserRole.Admin &&
                    _userContext.Role != UserRole.FrontDesk)
                {
                    throw new AccessDeniedException("You are not allowed to create appointments.");
                }

                // Facility scoping
                if (_userContext.Role == UserRole.FrontDesk &&
                    !string.Equals(_userContext.Clinic, request.Clinic, StringComparison.OrdinalIgnoreCase))
                {
                    throw new AccessDeniedException("You cannot create appointments for another clinic.");
                }

                // Time validation
                if (request.AppointmentTime < DateTime.Now)
                {
                    throw new RequestValidationException("Appointment time cannot be in the past.");
                }

                // Conflict check
                if (HasTimeConflict(request.Clinic, request.AppointmentTime))
                {
                    throw new RequestValidationException("An appointment already exists for this time slot.");
                }

                //Patient Code Valid
                var patientDetails = PatientService.Get(request.PatientCode);
                if (patientDetails == null)
                {
                    throw new RequestValidationException("Patient code does not exist.");
                }

                var appointment = new Appointment
                {
                    Id = nextId++,
                    Title = request.Title,
                    Clinic = request.Clinic,
                    Time = request.AppointmentTime,

                    PatientCode = request.PatientCode,
                    AppointmentStatus = AppointmentStatus.Open,
                    Patient = patientDetails
                };

                Appointments.Add(appointment);

                _logger.LogInformation(
                    "Appointment created successfully. Username={Username}, AppointmentId={Id}, Clinic={Clinic}, Time={Time}",
                    _userContext.Username,
                    appointment.Id,
                    appointment.Clinic,
                    appointment.Time);

                return appointment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating appointment");
                throw;
            }
        }


        private bool HasTimeConflict(string clinic, DateTime time)
        {
            return Appointments.Any(a =>
                a.Clinic.Equals(clinic, StringComparison.OrdinalIgnoreCase) &&
                a.Time == time);
        }
    }
}
