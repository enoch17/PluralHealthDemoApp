using PluralHealthDemoApp.Exceptions;
using PluralHealthDemoApp.Models;
using System.ComponentModel.DataAnnotations;

namespace PluralHealthDemoApp.Services
{
    public class PaymentService
    {
        private readonly UserContext _userContext;
        private readonly InvoiceService _invoiceService;
        private readonly AppointmentService _appointmentService;
        private readonly AppointmentStatusFlowService _appointmentStatusFlowService;
        private readonly ILogger<PaymentService> _logger;

        private static readonly List<Payment> Payments = new();

        public PaymentService(UserContext userContext, ILogger<PaymentService> logger, InvoiceService invoiceService, AppointmentStatusFlowService appointmentStatusFlowService, AppointmentService appointmentService)
        {
            _userContext = userContext;
            _logger = logger;
            _invoiceService = invoiceService;
            _appointmentStatusFlowService = appointmentStatusFlowService;
            _appointmentService = appointmentService;
        }

        public Payment ProcessPayment(CreatePaymentRequest request)
        {
            try
            {
                // Role enforcement
                if (_userContext.Role == UserRole.Staff)
                {
                    _logger.LogWarning(
                        "Payment denied. Role={Role}, Username={Username}",
                        _userContext.Role, _userContext.Username);

                    throw new AccessDeniedException("You are not allowed to process payments.");
                }

                Invoice invoice = _invoiceService.Get(request.InvoiceId);
                Appointment appointment = _appointmentService.Get(invoice.AppointmentId);

                // Facility scoping
                if (appointment.Clinic != _userContext.Clinic)
                    throw new AccessDeniedException("Cannot process payment for another clinic.");

                if (invoice.Status == InvoiceStatus.Paid)
                    throw new ValidationException("Invoice is already fully paid.");

                if (request.Amount != invoice.Total)
                    throw new ValidationException("Payment is not equal to outstanding balance.");

                // Create payment
                var payment = new Payment
                {
                    Id = Payments.Count + 1,
                    InvoiceId = invoice.Id,
                    Amount = request.Amount,
                    PaidAt = DateTime.UtcNow,
                    Clinic = appointment.Clinic,
                    PaymentMethod = request.PaymentMethod,
                    ReceivedBy = _userContext.Username
                };

                Payments.Add(payment);

                // Update invoice
                invoice.Status = InvoiceStatus.Paid;
                //
                _appointmentStatusFlowService.MoveToAwaitingVitals(_appointmentService.Get(invoice.AppointmentId), "Payment Made");

                _logger.LogInformation(
                    "Payment processed. Username={Username}, InvoiceId={InvoiceId}, Amount={Amount}, Status={Status}",
                    _userContext.Username, invoice.Id, request.Amount, invoice.Status);

                return payment;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error Process Payments");
                throw;
            }
            
        }
    }

}
