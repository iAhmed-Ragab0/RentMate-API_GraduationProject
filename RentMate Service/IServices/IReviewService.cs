using RentMate_Service.DTOs.Reviews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.IServices
{
    public interface IReviewService
    {
        public Task<IEnumerable<ReviewDTO_GetAll>> GetAllReviewsForProperty(int propId);
        public double GetAvgRatingForProperty(int id);
        public  Task<ReviewDTO_Post> AddReviewAsync(ReviewDTO_Post ReviewDTO);
        public Task<bool> UpdateReviewByIdAsync(int Reviewid, ReviewDTO_Update propertyDTO);
        public Task<Review> DeleteReviewByIdAsync(int id);



    }
}
