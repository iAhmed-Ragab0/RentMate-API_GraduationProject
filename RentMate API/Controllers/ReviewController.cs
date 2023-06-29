using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentMate_Service.DTOs.Reviews;
using RentMate_Service.DTOs.User;
using RentMate_Service.DTOs.WishingList;
using RentMate_Service.IServices;
using RentMate_Service.Services;

namespace RentMate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _ReviewService;

        public ReviewController(IReviewService reviewService)
        {
            _ReviewService = reviewService;
        }



        [HttpPost]

        public async Task<IActionResult> AddReview(ReviewDTO_Post Review)
        {
            try
            {
                await _ReviewService.AddReviewAsync(Review);
                return Ok();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpPut("{ReviewId}")]
        public async Task<ActionResult<bool>> UpdateUser(int ReviewId, ReviewDTO_Update upatedReview)
        {
            try
            {
                await _ReviewService.UpdateReviewByIdAsync(ReviewId, upatedReview);
                return Ok($"Review with id =  {ReviewId} is updated Succesfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteReviewById(int ReviewId)
        {

            try
            {
                var result = await _ReviewService.DeleteReviewByIdAsync(ReviewId);

                if (result != null)
                    return Ok($"Property with id =  {ReviewId} is Deleted Succesfully");
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
