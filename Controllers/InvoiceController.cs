using Microsoft.AspNetCore.Mvc;
using PluralHealthDemoApp.Exceptions;
using PluralHealthDemoApp.Models;
using PluralHealthDemoApp.Services;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly InvoiceService _invoiceService;

    public InvoiceController(InvoiceService invoiceService)
    {
        _invoiceService = invoiceService;
    }

    [HttpPost]
    public ActionResult<Invoice> Create(CreateInvoiceRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var invoice = _invoiceService.CreateInvoice(request);
            return Created("", invoice);
        }
        catch (NotFoundException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (RequestValidationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (AccessDeniedException ex)
        {
            return StatusCode(403, new { message = ex.Message });
        }
        catch (Exception)
        {
            return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
        }
    }
}
