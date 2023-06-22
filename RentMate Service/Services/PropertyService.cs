using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RentMate_Domain;
using RentMate_Repository._1__IRepositories;
using RentMate_Repository.UnitOfWork;
using RentMate_Service.DTOs;
using RentMate_Service.DTOs.Property;
using RentMate_Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.Services
{
    public class PropertyService : IPropertyService
    {
        private AppDbContext _DbContext;
        private IUnitOfWork _IUnitOfWork;
        private IPropertyRepository _PropertyRepository;
        private IPropertyDetailsReopsitory _PropertyDetailsRepository;


        public PropertyService(IUnitOfWork iUnitOfWork,
            IPropertyRepository propertyRepository,
            IPropertyDetailsReopsitory propertyDetailsRepository,
            AppDbContext dbContext
            )
        {
            _IUnitOfWork = iUnitOfWork;
            _PropertyRepository = propertyRepository;
            _PropertyDetailsRepository = propertyDetailsRepository;
            this._DbContext = dbContext;
        }
            

        //Get All Properties
        public async Task<IEnumerable<PropertyDTO_GetAll>> GetAllPropertiesAsync()
        {
            var includes = new Expression<Func<Propertyy, object>>[]
             {
                p => p.Owner,
                p => p.Photos
             };

            var properties = await _PropertyRepository.GetAllAsync(includes);

            List<PropertyDTO_GetAll> allProperties = new List<PropertyDTO_GetAll>();

            foreach (var property in properties)
            {
                PropertyDTO_GetAll prp = new PropertyDTO_GetAll();

                prp.Street = property.Street;
                prp.City = property.City;
                prp.Governorate = property.Governorate;
                prp.Id = property.Id;
                prp.PropertyType = property.PropertyType;
                prp.Title = property.Title;
                prp.Description = property.Description;
                prp.PropertyPrice = property.PropertyPrice;
                prp.IsRented = property.IsRented;
                //prp.AverageRating = AverageRating(property.ID);

                //prp.MainPhotoUrl = ReturnMainPhoto(property.Photos.ToList());

                // Owner data
                prp.OwnerPhoto = property.Owner.ProfileImg;
                prp.OwnerFullName = property.Owner.FirstName + ' ' + property.Owner.LastName;

                allProperties.Add(prp);
            }

            return allProperties;
        }

        //----------------------------------------------------------------------

        //Get property By ID
        public async Task<PropertyDTO_GetById> GetPropertyByIdAsync(int _id)
        {
            var includes = new Expression<Func<Propertyy, object>>[]
            {
                p => p.Owner,
                p => p.Photos,
                p => p.Details,
                p => p.Reviews,
            };

            var property = await _PropertyRepository.GetByIdAsync(_id, includes) ?? default;

            PropertyDTO_GetById propOwner = new PropertyDTO_GetById()
            {
                OwnerFullName = property.Owner.FirstName + ' ' + property.Owner.LastName,
                OwnerProfileImg = property.Owner.ProfileImg  ,
                OwnerEmail = property.Owner.Email,
                OwnerPhone = property.Owner.PhoneNumber,

                // Property 
                Id = property.Id,
                Title = property.Title,
                NoOfBedsInTheRoom = property.NoOfBedsInTheRoom,
                NoOfBedsPerApartment = property.NoOfBedsPerApartment,
                NoOfRooms = property.NoOfRooms,
                City = property.City,
                Governorate = property.Governorate,
                Description = property.Description,
                Street = property.Street,
                PropertyType = property.PropertyType,
                IsRented = property.IsRented,
                Photos = property.Photos.ToList(),
                hasKitchen = property.Details.hasKitchen,
                hasAirConditioner = property.Details.hasAirConditioner,
                hasMicrowave = property.Details.hasMicrowave,
                hasDishesAndSilverware = property.Details.hasDishesAndSilverware,
                hasWifi = property.Details.hasWifi,
                hasDishWasher = property.Details.hasDishWasher,
                hasParking = property.Details.hasParking,
                hasRefrigerator = property.Details.hasRefrigerator,
                hasWaterHeater = property.Details.hasWaterHeater,
                PropertyPrice = property.PropertyPrice,
                // Reviews
                Reviews = property.Reviews.ToList(),
                //AverageRating = AverageRating(property.Id),
            };

            return propOwner;
        }

        //----------------------------------------------------------------------
        // Get property By Owner ID
        public async Task<List<PropertyDTO_GetAll>> GetPropertiesByOwnerIdAsync(string id)
        {

            var properties = await _PropertyRepository.GetPropertiesByOwnerIdAsync(id);

            List<PropertyDTO_GetAll> allProperties = new List<PropertyDTO_GetAll>();

            foreach (var property in properties)
            {
                PropertyDTO_GetAll prp = new PropertyDTO_GetAll();

                prp.Id = property.Id;
                prp.Title = property.Title;
                prp.PropertyPrice = property.PropertyPrice;
                prp.PropertyType = property.PropertyType;
                prp.Governorate = property.Governorate;
                prp.City = property.City;
                prp.Street = property.Street;
                prp.Description = property.Description;
                prp.IsRented = property.IsRented;
                //prp.AverageRating = AverageRating(property.ID);
                //prp.MainPhotoUrl = ReturnMainPhoto(property.Photos.ToList());

                // Owner data
                prp.OwnerPhoto = property.Owner.ProfileImg;
                prp.OwnerFullName = property.Owner.FirstName + ' ' + property.Owner.LastName;

                allProperties.Add(prp);
            }

            return allProperties;

        }

        //----------------------------------------------------------------------
        // Add Property
        public async Task<PropertyDTO_Add> AddPropertyAsync(PropertyDTO_Add PropertyDTO)
        {

            Propertyy newProperty = new Propertyy()
            {
                OwnerId = PropertyDTO.OwnerId,
                Title = PropertyDTO.Title,
                PropertyType = PropertyDTO.PropertyType,
                PropertyPrice = PropertyDTO.PropertyPrice,
                Governorate = PropertyDTO.Governorate,
                City = PropertyDTO.City,
                Street = PropertyDTO.Street,
                Description = PropertyDTO.Description,
                NoOfBedsPerApartment = PropertyDTO.NoOfBedsPerApartment,
                NoOfBedsInTheRoom = PropertyDTO.NoOfBedsInTheRoom,
                NoOfRooms = PropertyDTO.NoOfRooms,
                StripeId = PropertyDTO.StripeId,
                Amount = PropertyDTO.Amount,
                IsRented = false,
                
            };

            // _DbContext.Add(newProperty);
            // _DbContext.SaveChanges();


            //PropertyDetails services = new PropertyDetails()
            //{
            //    PropertyId = newProperty.Id
            //};

            //_DbContext.AddAsync(services);
            //await _DbContext.SaveChangesAsync();

            //return PropertyDTO;

            await _PropertyRepository.AddAsync(newProperty);
            await _IUnitOfWork.Commit();


            PropertyDetails services = new PropertyDetails()
            {
                PropertyId = newProperty.Id
            };

            await _PropertyDetailsRepository.AddAsync(services);
            await _IUnitOfWork.Commit();

            return PropertyDTO;

        }

        //----------------------------------------------------------------------
        // Delete Property
        public async Task<Propertyy> DeletePropertyByIdAsync(int id)
        {
            Propertyy property = await _PropertyRepository.DeleteAsync(id);
            await _IUnitOfWork.Commit();

            if (property != null)
            {
                return property;
            }
            else
                return null;
        }

        //----------------------------------------------------------------------
        // Update Property
        public async Task<bool> UpdateProertyByIdAsync(int id , 
            PropertyDTO_Update propertyDTO)
        {
            var includes = new Expression<Func<Propertyy, object>>[]
            {
                p => p.Owner,
                p => p.Photos,
                p => p.Details,
                p => p.Reviews,
            };

            var existingProperty = await _PropertyRepository.GetByIdAsync(id, includes);

            if (existingProperty == null)
            {
                return false;
            }

            existingProperty.Title = propertyDTO.Title;
            existingProperty.Description = propertyDTO.Description;
            existingProperty.IsRented = propertyDTO.IsRented;
            existingProperty.NoOfBedsInTheRoom = propertyDTO.NoOfBedsInTheRoom;
            existingProperty.NoOfBedsPerApartment = propertyDTO.NoOfBedsPerApartment;

            var updatedProperty = await _PropertyRepository
                                        .UpdateAsync<int>(id, existingProperty, p => p.Id);
        
            await _IUnitOfWork.Commit();
            return true;
        }

    }

























        //function to return the Average Rating
        //public double AverageRating(int propertyId)
        //{
        //    var exists = _ontext.Reviews.FirstOrDefault(b => b.PropertyId == propertyId);
        //    if (exists == null)
        //    {
        //        return 0;
        //    }
        //    else
        //    {
        //        var avgRating = _context.Reviews.Where(r => r.PropertyId == propertyId).Average(a => a.Rating);
        //        return avgRating;
        //    }

        //}
        //public string ReturnMainPhoto(List<Photo> photos)
        //{
        //    foreach (var photo in photos)
        //    {
        //        if (photo.IsMain)
        //        {
        //            return photo.Url;
        //        }
        //    }
        //    return string.Empty;
        //}
    }





