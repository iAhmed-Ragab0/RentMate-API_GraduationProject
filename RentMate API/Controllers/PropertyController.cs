using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RentMate_Domain.Models;
using RentMate_Repository.IRepositories;
using RentMate_Repository.UnitOfWork;
using RentMate_Service.DTOs.Property;
using RentMate_Service.IServices;

namespace RentMate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {

        private readonly IPropertyService _PropertyService;

        public PropertyController(IPropertyService propertyService)
        {
                _PropertyService= propertyService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProperties()
        {
            try
            {
                var result = await _PropertyService.GetAllPropertiesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("id")]
        public async Task<ActionResult<PropertyDTO_GetById>> GetPropertyById(int id)

        {
            try
            {
                var result = await _PropertyService.GetPropertyByIdAsync(id) ?? default;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("Owner/{id}")]
        public async Task<ActionResult<List<PropertyDTO_GetAll>>> GetPropertyByOwnerId(string id)

        {
            try
            {
                var propertyById = await _PropertyService.GetPropertiesByOwnerIdAsync(id);

                return Ok(propertyById);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]

        public async Task<IActionResult> Add(PropertyDTO_Add propertyDTO)
        {
            try
            {
                await _PropertyService.AddPropertyAsync(propertyDTO);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }



        [HttpPut]
        public async Task<ActionResult> UpdatePropertyById(int id ,PropertyDTO_Update property)
        {
            var Result = _PropertyService.UpdateProertyByIdAsync(id, property);

            if ( Result != null )
                return Ok($"Property with id =  {id} is Updated Succesfully");
            else
                return BadRequest();
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePropertyById(int id)
        {
            var result = await _PropertyService.DeletePropertyByIdAsync(id);

            if ( result != null )
                return Ok($"Property with id =  { id } is Deleted Succesfully");
            else
                return BadRequest();
        }

    }
}
