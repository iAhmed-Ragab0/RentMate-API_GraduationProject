using Microsoft.AspNetCore.Mvc;
using RentMate_Service.DTOs;
using RentMate_Service.DTOs.Photo;
using RentMate_Service.DTOs.Property;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.IServices
{
    public interface IPropertyService 
    {
        public Task<IEnumerable<PropertyDTO_GetAll>> GetAllPropertiesAsync();

        public Task<PropertyDTO_GetById> GetPropertyByIdAsync(int id);
        public Task<List<PropertyDTO_GetAll>> GetPropertiesByOwnerIdAsync
                                              (string id);
        public Task<PropertyDTO_Add> AddPropertyAsync(PropertyDTO_Add PropertyDTO);

        public Task<bool> UpdateProertyByIdAsync
                                        (int id , PropertyDTO_Update property);

        public Task<Propertyy> DeletePropertyByIdAsync(int id);




    }
}
