using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentMate_Service.DTOs.User;
using RentMate_Service.IServices;

namespace RentMate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly IUserService _UserService;

        public UserController(IUserService userService)
        {
            _UserService = userService;
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

    }
}
