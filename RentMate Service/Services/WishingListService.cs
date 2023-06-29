using Microsoft.EntityFrameworkCore;
using RentMate_Repository._1__IRepositories;
using RentMate_Repository._2__Repositories;
using RentMate_Repository.UnitOfWork;
using RentMate_Service.DTOs.Appointment;
using RentMate_Service.DTOs.Property;
using RentMate_Service.DTOs.WishingList;
using RentMate_Service.IServices;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.Services
{
    public class WishingListService : IWishingListService
    {
        private IUnitOfWork _IUnitOfWork;
        private IWishingListRepository _WishingListRepository;
        private IReviewService _ReviewService;
        private IPhotoService _PhotoService;



        public WishingListService(IUnitOfWork iUnitOfWork, IWishingListRepository wishingListRepository, IReviewService reviewService, IPhotoService photoService)
        {
            _IUnitOfWork = iUnitOfWork;
            _WishingListRepository = wishingListRepository;
            _ReviewService = reviewService;
            _PhotoService = photoService;
        }




        //Get  Wishing Lists by user id
        public async Task<IEnumerable<WishingListDTO_Get>> GetWishListByUserIdAsync(string id)
        {
            var wishingList = await _WishingListRepository.GetWishListByUserId(id);


            var allwishingListDTO = new List<WishingListDTO_Get>();


            foreach (var wish in wishingList)
            {
                WishingListDTO_Get wishDTO = new WishingListDTO_Get()
                {
                    wishingId = wish.Id,
                    PropertyId = wish.Property.Id,
                    Title = wish.Property.Title,
                    AppartmentArea = wish.Property.AppartmentArea,
                    AverageRating = _ReviewService.GetAvgRatingForProperty(wish.Property.Id),
                    City= wish.Property.City.city_name_en,
                    Governorate = wish.Property.City.Governorate.governorate_name_en,
                    MainPhotoUrl = await _PhotoService.ReturnMainPhotoForProperty(wish.Property.Id),
                    NoOfBathroom = wish.Property.NoOfBathroom,
                    NoOfRooms = wish.Property.NoOfRooms,
                    PropertyPrice =  wish.Property.PropertyPrice,
                    PropertyType = wish.Property.PropertyType,
                    StreetDetails = wish.Property.Street,


                    OwnerPhoto = wish.Property.Owner.ProfileImg,
                    OwnerFullName = wish.Property.Owner.FirstName + ' ' + wish.Property.Owner.LastName,

                    

                };
                allwishingListDTO.Add(wishDTO);
            }

            return allwishingListDTO;



        }
        //-----------------------------------------------------------------------
        //Delete Wish
        public async Task<WishingList> DeleteWishByIdAsync(int id)
        {
            WishingList wishingList = await _WishingListRepository.DeleteAsync(id);

            await _IUnitOfWork.Commit();

            if (wishingList != null)
            {
                return wishingList;
            }
            else
                return null;
        }


        //-------------------------------------------------------------------------
        //Add Wish
        public async Task<WishingListDTO_Post> AddWish(WishingListDTO_Post wishDTO)
        {
            WishingList newAppointment = new WishingList()
            {

             UserId= wishDTO.UserId,
             PropertyId= wishDTO.PropertyId,

            };

            await _WishingListRepository.AddAsync(newAppointment);

            await _IUnitOfWork.Commit();
            return wishDTO;
        }



    }
}
