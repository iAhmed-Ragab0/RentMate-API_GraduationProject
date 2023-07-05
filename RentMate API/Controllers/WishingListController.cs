using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentMate_Domain.Models;
using RentMate_Service.DTOs.Property;
using RentMate_Service.DTOs.WishingList;
using RentMate_Service.IServices;
using RentMate_Service.Services;

namespace RentMate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishingListController : ControllerBase
    {
        private readonly IWishingListService _WishingListService;

        public WishingListController(IWishingListService propertyService)
        {
            _WishingListService = propertyService;
        }

        [HttpGet("User/{user}")]
        public async Task<ActionResult<IEnumerable<WishingListDTO_Get>>> GetAppointmentOwner([FromRoute] string user)
        {

            try
            {
                var wishingsList = await _WishingListService.GetWishListByUserIdAsync(user);

                return Ok(wishingsList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteWishyById(int id)
        {
            var result = await _WishingListService.DeleteWishByIdAsync(id);

            if (result != null)
                return Ok($"Property with id =  {id} is Deleted Succesfully");
            else
                return NotFound();
        }

        [HttpPost]

        public async Task<IActionResult> AddWish(WishingListDTO_Post wish)
        {
            try
            {
                await _WishingListService.AddWish(wish);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
