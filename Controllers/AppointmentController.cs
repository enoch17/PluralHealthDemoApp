using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PluralHealthDemoApp.Models;
using PluralHealthDemoApp.Services;
using PluralHealthDemoApp.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace PluralHealthDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;
        public AppointmentController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet]
        public ActionResult<PagedResult<Appointment>> GetAll(
            DateTime? startDate,
            DateTime? endDate,
            string? clinic,
            string? search,
            string? sort,
            int page = 1,
            int pageSize = 20)
        {
            try
            {
                var result = _appointmentService.GetAll(startDate, endDate, clinic, search, sort, page, pageSize);

                return Ok(result);
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
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later."});
            }
        }

        [HttpPost]
        public ActionResult<Appointment> Create(
        [FromBody] CreateAppointmentRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var appointment = _appointmentService.CreateAppointment(request);

                return CreatedAtAction(nameof(Get), new { id = appointment.Id }, appointment);
            }
            catch (RequestValidationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (AccessDeniedException ex)
            {
                return StatusCode(403, new {message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new {message = "An unexpected error occurred. Please try again later." });
            }
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Appointment> Get(int id)
        {
            try
            {
                var Appointment = _appointmentService.Get(id);
                if (Appointment == null)
                    return NotFound();
                return Appointment;
            }
            catch(NotFoundException ex)
            {
                return StatusCode(404, new { message = ex.Message });
            }
            catch (AccessDeniedException ex)
            {
                return StatusCode(403, new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later."});
            }
        }
    }
}
