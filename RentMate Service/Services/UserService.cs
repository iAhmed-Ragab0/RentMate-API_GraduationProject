using Microsoft.EntityFrameworkCore;
using RentMate_Domain.Models;
using RentMate_Repository._1__IRepositories;
using RentMate_Repository._2__Repositories;
using RentMate_Repository.UnitOfWork;
using RentMate_Service.DTOs.Appointment;
using RentMate_Service.DTOs.Property;
using RentMate_Service.DTOs.User;
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
    public class UserService : IUserService
    {
        private IUnitOfWork _IUnitOfWork;
        private IUserRepository _UserRepository;
        private IPhotoService _PhotoService;
        private IPropertyRepository _PropertyRepository;



        public UserService(IUnitOfWork iUnitOfWork,
            IUserRepository userRepository,
            IPhotoService photoService,
            IPropertyRepository propertyRepository)
        {
            _IUnitOfWork = iUnitOfWork;
            _UserRepository = userRepository;
            _PhotoService = photoService;
            _PropertyRepository = propertyRepository;
        }

        //Get All Users
        public async Task<IEnumerable<UserDTO_GetAll>> GetAllUsers()
        {

            //var includes = new Expression<Func<User, object>>[]
            // {
            // };

            var AllUsers =  await _UserRepository.GetAllAsync();

            List<UserDTO_GetAll> allProperties = new List<UserDTO_GetAll>();

            foreach (var user in AllUsers)
            {
                UserDTO_GetAll userDTO = new UserDTO_GetAll()
                {

                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.PhoneNumber,
                    Role = user.Role,
                    Address = user.Address,
                    Id= user.Id,
                    ProfileImg = user.ProfileImg
                };




                allProperties.Add(userDTO);
            }

            return allProperties;

        }
        //------------------------------------------------------------------
        //Get User By ID

        public async Task<User> GetUserByIdAsync(string id)
        {
            var user = _UserRepository.GetByIdAsync(id);
            return await user;

        }
        //-------------------------------------------------------------------
        // Hard Delete User By ID
        public async Task<User>  HardDeleteUserByIdAsync(string id)
        {
            User property = await _UserRepository.DeleteAsync(id);
            await _IUnitOfWork.Commit();

            if (property != null)
            {
                return property;
            }
            else
                return null;
        }

        //---------------------------------------------------------------------
        //SoftDelete
        public async Task<User> SoftDeleteUserByIdAsync(string id)
        {
            User property = await _UserRepository.SoftDeleteUserByIdAsync(id);
            await _IUnitOfWork.Commit();

            if (property != null)
            {
                return property;
            }
            else
                return null;
        }


        //-----------------------------------------------------------------------
        //update user
        public async Task<bool> UpdateUserByIdAsync(string id,
                        UserDTO_Update UserDTO)
        {
            //var includes = new Expression<Func<User, object>>[]
            //{
            //    p => p
            //};

            var existingUser= await _UserRepository.GetByIdAsync(id);

            if (existingUser == null)
            {
                return false;
            }

            existingUser.FirstName = UserDTO.FirstName;
            existingUser.LastName = UserDTO.LastName;
            existingUser.Email = UserDTO.Email;
            existingUser.PhoneNumber = UserDTO.Phone;
            existingUser.Address = UserDTO.Address;
            existingUser.ProfileImg = UserDTO.Address;



            var updatedProperty = await _UserRepository
                                        .UpdateAsync<string>(id, existingUser, p => p.Id);

            await _IUnitOfWork.Commit();
            return true;
        }




        //-----------------------------------------------------------------------------
        // add profile img for user in db
        public async Task<bool> AddProfileImgForUserindb(string id,string imgUrl)
        {
           var ImgUrl =  await _UserRepository.AddProfileImgForUser(id, imgUrl);


            if (ImgUrl == null)
                return false;
            else
                return true;
        }

        //---------------------------------------------------------
        //Get User Properties
        public async Task<List<UserPropertiesDTO_Getall>> GetUserProperties(string userId) 
        {

            var properties = await _PropertyRepository.GetPropertiesByOwnerIdAsync(userId);

            List<UserPropertiesDTO_Getall> allProperties = new List<UserPropertiesDTO_Getall>();

            foreach (var property in properties)
            {
                UserPropertiesDTO_Getall prp = new UserPropertiesDTO_Getall()
                {

                    PropertyId = property.Id,
                    mainPhotoUrl = await _PhotoService.ReturnMainPhotoForProperty(property.Id),
                    PropertyPrice = property.PropertyPrice,
                    PropertyTitle = property.Title,
                    Status= property.Status,
                    
                };

                allProperties.Add(prp);
            }

            return allProperties;   
        }


    }
}
