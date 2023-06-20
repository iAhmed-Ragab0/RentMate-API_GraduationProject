using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentMate_Service.DTOs.Admin;

namespace RentMate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddRole(RoleDTO_Add roleDto)
        {

            if (ModelState.IsValid)
            {
                IdentityRole role = new IdentityRole() { Name = roleDto.RoleName };

                IdentityResult identityResult = await roleManager.CreateAsync(role);
                if (identityResult.Succeeded)
                {
                    return Ok();
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

            return BadRequest();
        }
    }
}
