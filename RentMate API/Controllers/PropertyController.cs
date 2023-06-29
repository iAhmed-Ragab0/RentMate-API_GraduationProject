using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RentMate_Domain.Models;
using RentMate_Repository.IRepositories;
using RentMate_Repository.UnitOfWork;
using RentMate_Service.DTOs.Photo;
using RentMate_Service.DTOs.Property;
using RentMate_Service.IServices;
using RentMate_Service.Services;
using System.Collections.Immutable;

namespace RentMate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {

        private readonly IPropertyService _PropertyService;
        private readonly IPhotoService _PhotoService;


        public PropertyController(IPropertyService propertyService, IPhotoService photoService)
        {
        
            _PropertyService= propertyService;
            _PhotoService= photoService;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<PropertyDTO_GetById>> GetPropertyById(int id)

        {
            try
            {
                var result = await _PropertyService.GetPropertyByIdAsync(id) ?? default;
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();

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

        [HttpPost("{propId}/add-photos")]

        public async Task<IActionResult> AddPhotosToProperty(IFormFile file, int propId)
        {
            try
            {

               var property =  await _PropertyService.GetPropertyByIdAsync(propId);
                if (property == null) return NotFound();

                var result = await _PhotoService.AddPhotoToCloudAsync(file);

                if (result.Error != null) return BadRequest(result.Error.Message);
                
                var photo = new Photo 
                {
                    Url = result.SecureUri.AbsoluteUri,
                    PublicId = result.PublicId,
                    PropertyId= propId

                };

                if(property.Photos.Count == 0 ) 
                    photo.IsMain= true; 
                else 
                    photo.IsMain = false;

                var saved =  await _PhotoService.SavePhotoTodb(photo);  
                

                if (saved)
                    return Ok($"image with url = {photo.Url} added succesfully");
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpDelete("{photoId}/delete-photo")]

        public async Task<IActionResult> DeletePhotoForProperty( int photoId)
        {
            try
            {

                var photo = await _PhotoService.GetphotoById(photoId);
                if (photo == null) return NotFound();

                if (photo.IsMain) return BadRequest("You cannot delete your main photo");


                if (photo.PublicId != null)
                {
                    var result = await _PhotoService.DeletePhotoFromCloudAsync(photo.PublicId);

                    if (result.Error  != null) return BadRequest(result.Error.Message);
                }


                var result2 = await _PhotoService.deletePhotoFromdb(photoId);

                 return Ok($"image with id = {photoId} deleted succesfully");


            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpGet("Photos-Update/{propId}")]
        public async Task<ActionResult<List<photoDTO_Update>>> GetPropertyPhotosForUpdateById(int propId)

        {
            try
            {
                var result = await _PhotoService.GetPropertyPhotos_Update(propId);
                if (result != null)
                    return Ok(result);
                else
                    return NotFound();

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
