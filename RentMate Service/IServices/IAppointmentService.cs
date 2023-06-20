using RentMate_Service.DTOs.Appointment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.IServices
{
    public interface IAppointmentService
    {
        public Task<IEnumerable<AppointmentDTO>> GetAllAppointmentForOwner(string Ownerid);
        public Task<IEnumerable<AppointmentDTO>> GetAllAppointmentForTenant(string Tenantid);
        public Task<AppointmentDTO> AddAppointment(AppointmentDTO newAppointment);
        public Task<Appointment> DeleteAppointmentByIdAsync(int id);
        public Task<bool> UpdateAppointmentByIdAsync(int id, AppointmentDTO_Update AppointmentDTO);

    }
}
