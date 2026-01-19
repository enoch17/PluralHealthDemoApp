using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PluralHealthDemoApp.Models;
using PluralHealthDemoApp.Services;

namespace PluralHealthDemoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        //[HttpGet]
        //public ActionResult<List<Patient>> GetAll() => PatientService.GetAll();
        //[HttpGet]
        //[Route("{code}")]
        //public ActionResult<Patient> Get(string code)
        //{
        //    var patient = PatientService.Get(code);
        //    if (patient == null) 
        //        return NotFound(); 
        //    return patient;
        //}
    }
}
