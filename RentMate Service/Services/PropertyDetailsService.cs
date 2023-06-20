using Microsoft.EntityFrameworkCore;
using RentMate_Domain;
using RentMate_Repository._1__IRepositories;
using RentMate_Repository.UnitOfWork;
using RentMate_Service.DTOs.Property;
using RentMate_Service.DTOs.PropertyDetails;
using RentMate_Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.Services
{
    public class PropertyDetailsService : IPropertyDetailsService
    {
        private AppDbContext _DbContext;
        private IUnitOfWork _IUnitOfWork;
        private IPropertyDetailsReopsitory _PropertyDetailsRepository;

        public PropertyDetailsService(IUnitOfWork iUnitOfWork,
            IPropertyDetailsReopsitory propertyDetailsRepository,
                        AppDbContext dbContext)
        {
            _IUnitOfWork = iUnitOfWork;
            _PropertyDetailsRepository = propertyDetailsRepository;
            this._DbContext = dbContext;
        }

        public async Task<bool> UpdateProertyServicesByIdAsync(int id, PropertyDetailsDTO ServicesDTO)
        {

            var includes = new Expression<Func<PropertyDetails, object>>[]
            {
                p => p.Property,
            };

            //var existingServices = await _PropertyDetailsRepository.GetByIdAsync(id, includes);

            var existingServices = await _DbContext.PropertyDetails
                                                .FirstOrDefaultAsync(a=>a.Id == id);


            if (existingServices == null)
            {
                return false;
            }
            else 
            {

                existingServices.hasAirConditioner = ServicesDTO.hasAirConditioner;
                existingServices.hasMicrowave = ServicesDTO.hasMicrowave;
                existingServices.hasParking = ServicesDTO.hasParking;
                existingServices.hasRefrigerator = ServicesDTO.hasRefrigerator;
                existingServices.hasWifi = ServicesDTO.hasWifi;
                existingServices.hasDishWasher = ServicesDTO.hasDishWasher;
                existingServices.hasWaterHeater = ServicesDTO.hasWaterHeater;
                existingServices.hasKitchen = ServicesDTO.hasKitchen;
                existingServices.hasDishesAndSilverware = ServicesDTO.hasDishesAndSilverware;


                var updatedProperty = await _PropertyDetailsRepository
                                              .UpdateAsync<int>(id, existingServices, p => p.Id);

                await _IUnitOfWork.Commit();
                return true;

            } 

        }


    }
}
