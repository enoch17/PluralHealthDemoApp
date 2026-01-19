using Microsoft.Extensions.Logging;
using PluralHealthDemoApp.Exceptions;
using PluralHealthDemoApp.Models;

namespace PluralHealthDemoApp.Services
{
    public class InvoiceService
    {
        private readonly ILogger<InvoiceService> _logger;
        private readonly UserContext _userContext;
        private readonly AppointmentService _appointmentService;
        private static List<Invoice> Invoices { get; } = new();

        public InvoiceService(ILogger<InvoiceService> logger, UserContext userContext, AppointmentService appointmentService)
        {
            _logger = logger;
            _userContext = userContext;
            _appointmentService = appointmentService;
        }

        public Invoice Get(int id)
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

                var invoice = Invoices.FirstOrDefault(p => p.Id == id);

                if (invoice == null)
                {
                    _logger.LogWarning(
                        "Invoice not found. Id={InvoiceId}, Username={Username}",
                        id,
                        _userContext.Username);

                    throw new NotFoundException("Invoice not found.");
                }
                Appointment appointment = _appointmentService.Get(invoice.AppointmentId);

                //Facility Check
                if (!string.IsNullOrWhiteSpace(appointment.Clinic) &&
                    !string.Equals(_userContext.Clinic, appointment.Clinic, StringComparison.OrdinalIgnoreCase))
                {
                    _logger.LogWarning(
                        "Access denied. Role={Role}, Username={Username}, UserClinic={UserClinic}, RequestedClinic={RequestedClinic}",
                        _userContext.Role,
                        _userContext.Username,
                        _userContext.Clinic,
                        appointment.Clinic);

                    throw new AccessDeniedException("You do not have access to this clinic.");
                }
                return invoice;
            }
            catch (Exception ex) {
                _logger.LogError(ex, "Error getting Invoice");
                throw;
            }
        }


        public Invoice CreateInvoice(CreateInvoiceRequest request)
        {
            try
            {
                _logger.LogInformation(
                "Creating invoice. Username={Username}, Role={Role}",
                _userContext.Username,
                _userContext.Role);

                Appointment appoinmtnet = _appointmentService.Get(request.AppointmentId);

                // Role check
                if (_userContext.Role != UserRole.Admin &&
                    _userContext.Role != UserRole.FrontDesk)
                {
                    throw new AccessDeniedException("You are not allowed to create invoices.");
                }

                // Facility scoping
                if (_userContext.Role == UserRole.FrontDesk &&
                    !_userContext.Clinic.Equals(appoinmtnet.Clinic, StringComparison.OrdinalIgnoreCase))
                {
                    throw new AccessDeniedException("You cannot create invoices for another clinic.");
                }

                if (request.Items == null || request.Items.Count == 0)
                {
                    throw new RequestValidationException("Invoice must contain at least one service item.");
                }

                var items = request.Items.Select(i => new InvoiceItem
                {
                    ServiceName = i.ServiceName,
                    UnitPrice = i.UnitPrice,
                    Quantity = i.Quantity
                }).ToList();

                var subTotal = items.Sum(i => i.LineTotal);
                var discountAmount = subTotal * (request.DiscountPercentage / 100);
                var total = subTotal - discountAmount;

                var invoice = new Invoice
                {
                    Id = Invoices.Count + 1,
                    InvoiceNumber = $"INV-{DateTime.UtcNow.Ticks}",
                    AppointmentId = request.AppointmentId,
                    CreatedDate = DateTime.Now,
                    SubTotal = subTotal,
                    DiscountAmount = discountAmount,
                    Total = total,
                    Items = items
                };

                Invoices.Add(invoice);

                _logger.LogInformation(
                    "Invoice created successfully. Username={Username}, InvoiceNumber={InvoiceNumber}, Total={Total}",
                    _userContext.Username,
                    invoice.InvoiceNumber,
                    invoice.Total);

                return invoice;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating Invoice");
                throw;
            }
            
        }
    }
}
