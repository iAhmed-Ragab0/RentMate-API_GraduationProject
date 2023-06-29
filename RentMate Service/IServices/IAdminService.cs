using RentMate_Service.DTOs.Property;
using RentMate_Service.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.IServices
{
    public interface IAdminService
    {
        public Task<IEnumerable<UserDTO_GetAll>> GetAllUser();
        public  Task DeleteUser(string userId);
        public Task<IEnumerable<PropertyDTO_GetAll>> GetAllProperties();
        public Task<PropertyDTO_GetById> GetPropertyById(int propId);
        public Task<IEnumerable<PropertyDTO_GetAll>> GetPropertiesByOwnerId(string ownerId);


    }
}
