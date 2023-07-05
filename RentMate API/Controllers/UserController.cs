using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentMate_Domain.Models;
using RentMate_Service.DTOs.Property;
using RentMate_Service.DTOs.User;
using RentMate_Service.IServices;
using RentMate_Service.Services;

namespace RentMate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly IUserService _UserService;
        private readonly IPhotoService _PhotoService;
        private readonly IPropertyService _PropertyService;



        public UserController(IUserService userService, IPhotoService photoService)
        {
            _UserService = userService;
            _PhotoService = photoService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var result = await _UserService.GetAllUsers();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpGet("id")]
        public async Task<IActionResult> GetUserById(string id)
        {
            try
            {
                var result = await _UserService.GetUserByIdAsync(id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete("hard/{id}")]
        public async Task<ActionResult> HardDeleteUserById(string id)
        {
            try
            {
                await _UserService.HardDeleteUserByIdAsync(id);
                return Ok($"User with id =  {id} is hard Deleted Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        [HttpDelete("soft/{id}")]
        public async Task<ActionResult> SoftDeleteUserById(string id)
        {
            try
            {
                await _UserService.SoftDeleteUserByIdAsync(id);
                return Ok($"User with id =  {id} is Soft Deleted Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("user/{id}")]
        public async Task<ActionResult<bool>> UpdateUser(string id,UserDTO_Update user)
        {
            try
            {
                await _UserService.UpdateUserByIdAsync(id, user);
                return Ok($"User with id =  {id} is updated Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }




        [HttpPost("{userId}/add-profileImg")]

        public async Task<IActionResult> AddProfileImgByUserId(IFormFile file, string userId)
        {
            try
            {

                var user = await _UserService.GetUserByIdAsync(userId);
                if (user == null) return NotFound();

                var result = await _PhotoService.AddPhotoToCloudAsync(file);

                if (result.Error != null) return BadRequest(result.Error.Message);




                var saved = await _UserService.AddProfileImgForUserindb(userId, result.SecureUri.AbsoluteUri);


                if (saved)
                    return Ok(result.SecureUri.AbsoluteUri);
                else
                    return BadRequest();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpGet("MyProperties/{userId}")]
        public async Task<ActionResult<List<UserPropertiesDTO_Getall>>> GetUserProperties(string userId)

        {
            try
            {
                var UserProperties = await _UserService.GetUserProperties(userId);

                return Ok(UserProperties);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPut("MyProperties/{userId}")]
        //public async Task<ActionResult<List<UserPropertiesDTO_Getall>>> EditPropertiesPhotos(string userId)

        //{
        //    try
        //    {
        //        var UserProperties = await _UserService.GetUserProperties(userId);

        //        return Ok(UserProperties);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}

    }
}
