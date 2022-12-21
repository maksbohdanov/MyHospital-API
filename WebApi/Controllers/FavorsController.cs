using BLL.Exceptions;
using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavorsController : ControllerBase
    {
        private readonly IFavorService _favorService;
        public FavorsController(IFavorService favorService)
        {
            _favorService= favorService;
        }

        [HttpGet("{favorId}")]
        public async Task<IActionResult> GetById(int favorId)
        {
            try
            {
                var favor = await _favorService.GetByIdAsync(favorId);
                return Ok(favor);
            }
            catch (HospitalException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var favors = await _favorService.GetAllAsync();

            return Ok(favors);
        }

        [HttpGet("specialization/{specialization}")]
        public async Task<IActionResult> FindBySpecialization(string specialization)
        {
            var favors = await _favorService.FindBySpecializationAsync(specialization);

            return Ok(favors);
        }
    }
}
