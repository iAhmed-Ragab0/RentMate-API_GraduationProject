using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentMate_Service.DTOs.Reviews;
using RentMate_Service.IServices;

namespace RentMate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _CityService;

        public CityController(ICityService cityService)
        {
            _CityService = cityService;
        }

        [HttpGet("{GovId}")]
        public async Task<ActionResult<List<string>>> UpdateUser(int GovId)
        {
            try
            {
                var allCitiesInGov = await _CityService.GetAllCitiesForGovernrate(GovId);
                return Ok(allCitiesInGov);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
