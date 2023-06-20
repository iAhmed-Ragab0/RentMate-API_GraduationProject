using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentMate_Service.DTOs.PropertyDetails;
using RentMate_Service.IServices;
using RentMate_Service.Services;

namespace RentMate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyDetailsController : ControllerBase
    {

        private readonly IPropertyDetailsService _PropertyServices_Service;

        public PropertyDetailsController(IPropertyDetailsService propertyServices_Service)
        {
            _PropertyServices_Service = propertyServices_Service;
        }


        [HttpPut]
        public IActionResult UpdateProertyServices(int id, PropertyDetailsDTO ServicesDTO)
        {
            if (ModelState != null)
            {

                bool IsUpdated = _PropertyServices_Service
                                    .UpdateProertyServicesByIdAsync(id, ServicesDTO).Result;
                if (IsUpdated)
                    return Ok();
                else 
                    return BadRequest();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
    }
}
