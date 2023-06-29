using RentMate_Repository._1__IRepositories;
using RentMate_Repository._2__Repositories;
using RentMate_Repository.UnitOfWork;
using RentMate_Service.DTOs.Property;
using RentMate_Service.DTOs.Reviews;
using RentMate_Service.DTOs.User;
using RentMate_Service.DTOs.WishingList;
using RentMate_Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.Services
{
    public class ReviewService : IReviewService
    {
        private IUnitOfWork _IUnitOfWork;
        private IReviewRepository _ReviewRepository;

        public ReviewService(IUnitOfWork iUnitOfWork, IReviewRepository reviewRepository)
        {
            _IUnitOfWork = iUnitOfWork;
            _ReviewRepository = reviewRepository;
        }


        //Get All Reviews for property
        public async Task<IEnumerable<ReviewDTO_GetAll>> GetAllReviewsForProperty(int propId)
        {

            var PropertyReviews = await _ReviewRepository.GetAllReviewsForProperty(propId);


            if (PropertyReviews.Any())
            {
                List<ReviewDTO_GetAll> PropertyReviewList = new List<ReviewDTO_GetAll>();

                foreach (var rev in PropertyReviews)
                {
                    ReviewDTO_GetAll revDTO = new ReviewDTO_GetAll()
                    {
                        ReviewId = rev.Id,
                        UserId = rev.UserId,
                        PropertyId = rev.PropertyId,
                        Comment = rev.Comment,
                        Rating = rev.Rating,

                    };

                    PropertyReviewList.Add(revDTO);
                }

                return PropertyReviewList;
            }
            else
            {
                return Enumerable.Empty<ReviewDTO_GetAll>();
            }

        }
        //-------------------------------------------------------------------------------------
        //Get AVG rating For Property

        public double GetAvgRatingForProperty(int id) 
        {
            var AvgRating=  _ReviewRepository.GetAvgRatingForProperty(id);

            if(AvgRating == 0)
              return 0;
            else
                return AvgRating;
        
        }

        //------------------------------------------------------------------------------------
        //Add review
        public async Task<ReviewDTO_Post> AddReviewAsync(ReviewDTO_Post ReviewDTO)
        {
            Review newReview = new Review()
            {

                UserId = ReviewDTO.UserId,
                PropertyId = ReviewDTO.PropertyId,
                Comment = ReviewDTO.Comment,
                Rating = ReviewDTO.Rating

            };

            await _ReviewRepository.AddAsync(newReview);

            await _IUnitOfWork.Commit();
            return ReviewDTO;
        }

        //----------------------------------------------------------------------
        // Update Property
        public async Task<bool> UpdateReviewByIdAsync(int Reviewid,
            ReviewDTO_Update propertyDTO)
        {

            var existingReview = await _ReviewRepository.GetByIdAsync(Reviewid);

            if (existingReview == null)
            {
                return false;
            }

            existingReview.Comment = propertyDTO.Comment;
            existingReview.Rating = propertyDTO.Rating;


            var updatedProperty = await _ReviewRepository
                                        .UpdateAsync<int>(Reviewid, existingReview, p => p.Id);

            await _IUnitOfWork.Commit();
            return true;
        }

        //----------------------------------------------------------------------
        // Delete Review
        public async Task<Review> DeleteReviewByIdAsync(int id)
        {
            Review wishingList = await _ReviewRepository.DeleteAsync(id);

            await _IUnitOfWork.Commit();

            if (wishingList != null)
            {
                return wishingList;
            }
            else
                return null;
        }

    }
}
