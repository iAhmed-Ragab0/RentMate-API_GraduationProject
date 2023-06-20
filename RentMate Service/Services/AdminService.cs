using RentMate_Repository._1__IRepositories;
using RentMate_Service.DTOs.User;
using RentMate_Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUserService _userService;
        private readonly IAppointmentService _appointmentService;
        private readonly IAdminRepository _adminRepository;

        public AdminService(
            IUserService userService,
            IAppointmentService appointmentService,
            IAdminRepository adminRepository)
        {
            _userService = userService;
            _appointmentService = appointmentService;
            _adminRepository = adminRepository;
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

    }
}
