using Microsoft.AspNetCore.Mvc;
using PluralHealthDemoApp.Exceptions;
using PluralHealthDemoApp.Models;
using PluralHealthDemoApp.Services;
using System.ComponentModel.DataAnnotations;

namespace PluralHealthDemoApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly PaymentService _paymentService;
        public PaymentController(PaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpPost]
        public ActionResult<Payment> ProcessPayment([FromBody] CreatePaymentRequest request)
        {
            try
            {
                var payment = _paymentService.ProcessPayment(request);
                return Ok(payment);
            }
            catch (AccessDeniedException ex)
            {
                return Forbid(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
        }

    }
}
