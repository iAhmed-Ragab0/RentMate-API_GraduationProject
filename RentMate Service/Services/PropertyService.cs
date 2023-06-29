using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RentMate_Domain;
using RentMate_Domain.Models;
using RentMate_Repository._1__IRepositories;
using RentMate_Repository._2__Repositories;
using RentMate_Repository.UnitOfWork;
using RentMate_Service.DTOs;
using RentMate_Service.DTOs.Property;
using RentMate_Service.DTOs.Reviews;
using RentMate_Service.IServices;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RentMate_Service.Services
{
    public class PropertyService : IPropertyService
    {
        private IUnitOfWork _IUnitOfWork;
        private IPropertyRepository _PropertyRepository;
        private IPropertyDetailsReopsitory _PropertyDetailsRepository;
        private IReviewService _ReviewService;
        private IPhotoService _PhotoService;



        public PropertyService(IUnitOfWork iUnitOfWork,
            IPropertyRepository propertyRepository,
            IPropertyDetailsReopsitory propertyDetailsRepository,
            IReviewService reviewService,
            IPhotoService photoService
            )
        {
            _IUnitOfWork = iUnitOfWork;
            _PropertyRepository = propertyRepository;
            _PropertyDetailsRepository = propertyDetailsRepository;
            _ReviewService = reviewService;
        _PhotoService = photoService;
        }
            

        //Get All Properties
        public async Task<IEnumerable<PropertyDTO_GetAll>> GetAllPropertiesAsync()
        {
            var includes = new Expression<Func<Propertyy, object>>[]
             {
                p => p.Owner,
                p => p.Photos,
                p=>p.City,
                p=>p.City.Governorate
             };

            var properties = await _PropertyRepository.GetAllAsync(includes);

            List<PropertyDTO_GetAll> allProperties = new List<PropertyDTO_GetAll>();

            foreach (var property in properties)
            {
                PropertyDTO_GetAll prp = new PropertyDTO_GetAll()
                {
                    StreetDetails = property.Street,
                    City = property.City.city_name_en,
                    Governorate = property.City.Governorate.governorate_name_en,
                    Id = property.Id,
                    PropertyType = property.PropertyType,
                    Title = property.Title,
                    PropertyPrice = property.PropertyPrice,
                    AppartmentArea = property.AppartmentArea,
                    NoOfRooms = property.NoOfRooms,
                    NoOfBathroom = property.NoOfBathroom,
                    AverageRating = _ReviewService.GetAvgRatingForProperty(property.Id),
                    MainPhotoUrl = await _PhotoService.ReturnMainPhotoForProperty(property.Id) ,

                    // Owner data
                    OwnerPhoto = property.Owner.ProfileImg,
                    OwnerFullName = property.Owner.FirstName + ' ' + property.Owner.LastName,

                };

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
                p=>p.City,
                p=>p.City.Governorate
            };

            var property = await _PropertyRepository.GetByIdAsync(_id, includes) ?? default;
            if (property == null)
                return null;
            else
            {

                PropertyDTO_GetById propOwner = new PropertyDTO_GetById()
                {
                    //owner
                    OwnerId= property.Owner.Id,
                    OwnerFullName = property.Owner.FirstName + ' ' + property.Owner.LastName,
                    OwnerProfileImg = property.Owner.ProfileImg,
                    OwnerEmail = property.Owner.Email,
                    OwnerPhone = property.Owner.PhoneNumber,
                    OwnerAddress= property.Owner.Address,

                    // Property 
                    Id = property.Id,
                    Title = property.Title,
                    NoOfBedsInTheRoom = property.NoOfBedsInTheRoom,
                    NoOfBedsPerApartment = property.NoOfBedsPerApartment,
                    NoOfRooms = property.NoOfRooms,
                    Description = property.Description,
                    PropertyType = property.PropertyType,
                    IsRented = property.IsRented,
                    Photos = (await _PhotoService.GetAllPhotosForProperty(_id))
                                                  .ToList(),
                    NoOfBathroom = property.NoOfBathroom,
                    AppartmentArea = property.AppartmentArea,
                    FloorNumber= property.FloorNumber,
                    
                    //Address
                    City = property.City.city_name_en,
                    Governorate = property.City.Governorate.governorate_name_en,
                    Street = property.Street,

                    //details
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
                    hasElevator = property.Details.hasElevator,
                        
                    // Reviews
                    Reviews = (await _ReviewService.GetAllReviewsForProperty(_id)).ToList(),
                    AverageRating = _ReviewService.GetAvgRatingForProperty(_id),
                };

                return propOwner;
            }
        }

        //----------------------------------------------------------------------
        // Get property By Owner ID
        public async Task<List<PropertyDTO_GetAll>> GetPropertiesByOwnerIdAsync(string id)
        {

            var properties = await _PropertyRepository.GetPropertiesByOwnerIdAsync(id);

            List<PropertyDTO_GetAll> allProperties = new List<PropertyDTO_GetAll>();

            foreach (var property in properties)
            {
                PropertyDTO_GetAll prp = new PropertyDTO_GetAll()
                {
                    StreetDetails = property.Street,
                    City = property.City.city_name_en,
                    Governorate = property.City.Governorate.governorate_name_en,
                    Id = property.Id,
                    PropertyType = property.PropertyType,
                    Title = property.Title,
                    PropertyPrice = property.PropertyPrice,
                    AppartmentArea = property.AppartmentArea,
                    NoOfRooms = property.NoOfRooms,
                    NoOfBathroom = property.NoOfBathroom,
                    AverageRating = _ReviewService.GetAvgRatingForProperty(property.Id),
                    MainPhotoUrl = await _PhotoService.ReturnMainPhotoForProperty(property.Id),

                    // Owner data
                    OwnerPhoto = property.Owner.ProfileImg,
                    OwnerFullName = property.Owner.FirstName + ' ' + property.Owner.LastName,

                };

                allProperties.Add(prp);
            }

            return allProperties;

        }

        //----------------------------------------------------------------------
        // Add Property
        public async Task<PropertyDTO_Add> AddPropertyAsync(PropertyDTO_Add PropertyDTO)
        {


                
            await _IUnitOfWork.BeginTransactionAsync();

                try
                {
                    var PropDetails = new PropertyDetails()
                    {
                        hasAirConditioner = PropertyDTO.hasAirConditioner,
                        hasMicrowave = PropertyDTO.hasMicrowave,
                        hasParking = PropertyDTO.hasParking,
                        hasRefrigerator = PropertyDTO.hasRefrigerator,
                        hasWifi = PropertyDTO.hasWifi,
                        hasDishWasher = PropertyDTO.hasDishWasher,
                        hasWaterHeater = PropertyDTO.hasWaterHeater,
                        hasKitchen = PropertyDTO.hasKitchen,
                        hasDishesAndSilverware = PropertyDTO.hasDishesAndSilverware,
                         hasElevator = PropertyDTO.hasElivator
                    };

                    Propertyy newProperty = new Propertyy()
                    {
                        OwnerId = PropertyDTO.OwnerId,
                        Title = PropertyDTO.Title,
                        PropertyType = PropertyDTO.PropertyType,
                        PropertyPrice = PropertyDTO.PropertyPrice,
                        CityId = PropertyDTO.CityId,
                        Street = PropertyDTO.StreetDetails,
                        Description = PropertyDTO.Description,
                        NoOfBedsPerApartment = PropertyDTO.NoOfBedsPerApartment,
                        NoOfBedsInTheRoom = PropertyDTO.NoOfBedsInTheRoom,
                        AppartmentArea = PropertyDTO.AppartmentArea,
                        NoOfRooms = PropertyDTO.NoOfRooms,
                        NoOfBathroom = PropertyDTO.NoOfBathroom,
                        IsRented = false,
                        Details = PropDetails,
                        FloorNumber = PropertyDTO.FloorNumber,
                        


                    };

                    var savedProp = await _PropertyRepository.AddAsync(newProperty);
                    await _IUnitOfWork.Commit();

                    // Assign the saved PropertyId to PropertyDetails
                    newProperty.Details.PropertyId = savedProp.Id;
                    await _IUnitOfWork.Commit();

                    // if stripe done ? 
                     await _IUnitOfWork.CommitTransactionAsync();
                    //else
                    //await _IUnitOfWork.RollbackTransactionAsync();

                    return PropertyDTO;

                }
                catch
                {
                    await _IUnitOfWork.RollbackTransactionAsync();
                    throw; // Rethrow the exception to be handled at the higher level
                }
            

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
            existingProperty.Details.hasKitchen = propertyDTO.hasKitchen;
            existingProperty.Details.hasAirConditioner = propertyDTO.hasAirConditioner;
            existingProperty.Details.hasMicrowave = propertyDTO.hasMicrowave;
            existingProperty.Details.hasDishWasher = propertyDTO.hasMicrowave;
            existingProperty.Details.hasWifi = propertyDTO.hasWifi;
            existingProperty.Details.hasRefrigerator = propertyDTO.hasRefrigerator;
            existingProperty.Details.hasDishesAndSilverware = propertyDTO.hasDishesAndSilverware;
            existingProperty.Details.hasParking = propertyDTO.hasParking;
            existingProperty.Details.hasWaterHeater = propertyDTO.hasWaterHeater;
            existingProperty.Details.hasElevator = propertyDTO.hasElevator;





            var updatedProperty = await _PropertyRepository
                                        .UpdateAsync<int>(id, existingProperty, p => p.Id);
        
            await _IUnitOfWork.Commit();
            return true;
        }





    }

























   
}





