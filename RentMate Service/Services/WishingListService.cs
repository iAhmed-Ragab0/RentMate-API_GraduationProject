using Microsoft.EntityFrameworkCore;
using RentMate_Repository._1__IRepositories;
using RentMate_Repository._2__Repositories;
using RentMate_Repository.UnitOfWork;
using RentMate_Service.DTOs.Appointment;
using RentMate_Service.DTOs.Property;
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
    public class WishingListService : IWishingListService
    {
        private IUnitOfWork _IUnitOfWork;
        private IWishingListRepository _WishingListRepository;

        public WishingListService(IUnitOfWork iUnitOfWork, IWishingListRepository wishingListRepository)
        {
            _IUnitOfWork = iUnitOfWork;
            _WishingListRepository = wishingListRepository;
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

                  PropertyTitle = wish.Property.Title,
                  PropertyId= wish.Property.Id,
                  //PropertyPhoto = wish.Property.Photos.
                  PropertyPrice = wish.Property.PropertyPrice,
                  UserId = wish.UserId

                };
                allwishingListDTO.Add(wishDTO);
            }

            return allwishingListDTO;



        }
        //-----------------------------------------------------------------------
        //Delete Appointment
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

            //await _IUnitOfWork.Commit();
            return wishDTO;
        }



    }
}
