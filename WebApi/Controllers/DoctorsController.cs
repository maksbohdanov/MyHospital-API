using BLL.Exceptions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService= doctorService;
        }

        [HttpGet("{doctorId}")]
        public async Task<IActionResult> GetById(int doctorId)
        {
            try
            {
                var doctor = await _doctorService.GetByIdAsync(doctorId);
                return Ok(doctor);
            }
            catch (HospitalException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctors = await _doctorService.GetAllAsync();

            return Ok(doctors);
        }

        [HttpGet("filter/{filter}")]
        public async Task<IActionResult> FindByFilter(string filter)
        {
            var doctors = await _doctorService.FindByFilterAsync(filter);

            return Ok(doctors);
        }
    }
}
