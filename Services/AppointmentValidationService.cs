using PluralHealthDemoApp.Models;

namespace PluralHealthDemoApp.Services
{
    public static class AppointmentValidationService
    {
        private static readonly string[] AllowedClinics =
        {
            "Ikeja",
            "Lekki"
        };

        public static ValidationResult ValidateClinic(string? clinic)
        {
            if (string.IsNullOrWhiteSpace(clinic))
                return ValidationResult.Success();

            if (!AllowedClinics.Contains(clinic, StringComparer.OrdinalIgnoreCase))
                return ValidationResult.Fail($"Clinic '{clinic}' is not valid., Valid Clinics are {String.Join(", ", AllowedClinics)}");

            return ValidationResult.Success();
        }

        public static ValidationResult ValidateTimeWindow(
            DateTime? from,
            DateTime? to)
        {
            if (from.HasValue && to.HasValue && from > to)
                return ValidationResult.Fail("Start time cannot be later than end time.");

            if (from.HasValue && to.HasValue &&
                (to.Value - from.Value).TotalDays > 31)
                return ValidationResult.Fail("Time range cannot exceed 31 days.");

            return ValidationResult.Success();
        }
    }
}
