using RentMate_Repository._1__IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Repository._2__Repositories
{
    public class AdminRepository :  IAdminRepository
    {
        //private readonly IAppointmentRepository _appointmentRepository;

        public AdminRepository(IAppointmentRepository appointmentRepository)
        {
            //_appointmentRepository = appointmentRepository;
        }


        //public async Task DeleteAppointment(int appointmentId)
        //{
        //    await _appointmentRepository.DeleteAsync(appointmentId);
        //}

    }
}
