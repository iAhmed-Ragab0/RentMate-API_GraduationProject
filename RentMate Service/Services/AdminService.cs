using RentMate_Repository._1__IRepositories;
using RentMate_Service.DTOs.Property;
using RentMate_Service.DTOs.User;
using RentMate_Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserService _userService;
        private readonly IAppointmentService _appointmentService;
        private readonly IPropertyService _PropertyService;
        private readonly IAdminRepository _adminRepository;

        public AdminService(
            IUserService userService,
            IAppointmentService appointmentService,
            IAdminRepository adminRepository,
            IPropertyService propertyService)
        {
            _userService = userService;
            _appointmentService = appointmentService;
            _adminRepository = adminRepository;
            _PropertyService = propertyService;

        }

        public async Task<IEnumerable<UserDTO_GetAll>> GetAllUser()
        {
             var result =    await _userService.GetAllUsers();

            return result;

        }

        public async Task DeleteUser(string userId)
        {
            await _userService.SoftDeleteUserByIdAsync(userId);
        }


        public async Task<IEnumerable<PropertyDTO_GetAll>> GetAllProperties()
        {
            var result = await _PropertyService.GetAllPropertiesAsync();

            return result;

        }


        public async Task<PropertyDTO_GetById> GetPropertyById(int propId)
        {
            var result = await _PropertyService.GetPropertyByIdAsync(propId);

            return result;

        }


        public async Task<IEnumerable<PropertyDTO_GetAll>> GetPropertiesByOwnerId(string ownerId)
        {
            var result = await _PropertyService.GetPropertiesByOwnerIdAsync(ownerId);

            return result;

        }

    }
}
