using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentMate_Service.IServices;
using RentMate_Service.Services;

namespace RentMate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _AdminService;

        public AdminController(IAdminService adminService)
        {
            _AdminService = adminService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllProperties()
        {
            try
            {
                var result = await _AdminService.GetAllUser();
                return Ok(result);
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
                await _AdminService.DeleteUser(id);
                return Ok($"User with id =  {id} is Soft Deleted Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
    }
}
