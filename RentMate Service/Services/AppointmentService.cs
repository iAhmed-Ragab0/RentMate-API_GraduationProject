using RentMate_Domain;
using RentMate_Domain.Models;
using RentMate_Repository._1__IRepositories;
using RentMate_Repository.UnitOfWork;
using RentMate_Service.DTOs.Appointment;
using RentMate_Service.DTOs.Property;
using RentMate_Service.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static RentMate_Domain.Constant.Enums;

namespace RentMate_Service.Services
{
    public class AppointmentService : IAppointmentService
    {
        private IUnitOfWork _IUnitOfWork;
        private IAppointmentRepository _AppintmentRepository;

        public AppointmentService(IUnitOfWork iUnitOfWork, IAppointmentRepository AppointmentRepository)
        {
            _IUnitOfWork = iUnitOfWork;
            _AppintmentRepository = AppointmentRepository;
        }


        // Get all appointments for owner 
        public async Task<IEnumerable<AppointmentDTO>> GetAllAppointmentForOwner(string Ownerid)
        {
            var AppointmentList = await _AppintmentRepository.GetAllAppointmentForOwner(Ownerid);

            var allAppointmentsDTO = new List<AppointmentDTO>();


            foreach (var appointment in AppointmentList)
            {
                AppointmentDTO app = new AppointmentDTO();

                app.PropertyId= appointment.PropertyId;
                app.OwnerId= appointment.OwnerId;
                app.TenantId= appointment.TenantId;
                app.TourDay= appointment.TourDay;
                app.TourHour= appointment.TourHour;
                app.Message = appointment.Message;

                allAppointmentsDTO.Add(app);
            }
            
            return allAppointmentsDTO;
        }
        //-------------------------------------------------------------
        //Get all appointments for tenant
        public async Task<IEnumerable<AppointmentDTO>> GetAllAppointmentForTenant(string tenantid)
        {
            var AppointmentList = await _AppintmentRepository.GetAllAppointmentForTenant(tenantid);

            var allAppointmentsDTO = new List<AppointmentDTO>();


            foreach (var appointment in AppointmentList)
            {
                AppointmentDTO app = new AppointmentDTO();

                app.PropertyId = appointment.PropertyId;
                app.OwnerId = appointment.OwnerId;
                app.TenantId = appointment.TenantId;
                app.TourDay = appointment.TourDay;
                app.TourHour = appointment.TourHour;
                app.Message = appointment.Message;

                allAppointmentsDTO.Add(app);
            }

            return allAppointmentsDTO;
        }
        //-------------------------------------------------------------
        //Add Appointment
        public async Task<AppointmentDTO> AddAppointment(AppointmentDTO AppointmentDTO)
        {
            Appointment newAppointment = new Appointment()
            {

                OwnerId = AppointmentDTO.OwnerId,
                TenantId = AppointmentDTO.TenantId,
                Message = AppointmentDTO.Message,
                //reservationstatus = ReservationStatus.Pending,
                TourHour = AppointmentDTO.TourHour,
                TourDay = AppointmentDTO.TourDay,
                PropertyId = AppointmentDTO.PropertyId,
                
             };

            await _AppintmentRepository.AddAsync(newAppointment);
            //await _IUnitOfWork.Commit();
            return AppointmentDTO;
        }

        //-------------------------------------------------------------
        //Delete Appointment
        public async Task<Appointment> DeleteAppointmentByIdAsync(int id)
        {
            Appointment property = await _AppintmentRepository.DeleteAsync(id);
            await _IUnitOfWork.Commit();

            if (property != null)
            {
                return property;
            }
            else
                return null;
        }

        //-------------------------------------------------------------
        // Update apointment
        public async Task<bool> UpdateAppointmentByIdAsync(int id,
           AppointmentDTO_Update AppointmentDTO)
        {
            var includes = new Expression<Func<Appointment, object>>[]
            {
                p => p.Owner,
            };

            var existingAppointment = await _AppintmentRepository.GetByIdAsync(id, includes);

            if (existingAppointment == null)
            {
                return false;
            }

            existingAppointment.reservationstatus = AppointmentDTO.reservationstatus;


            var updatedProperty = await _AppintmentRepository
                                        .UpdateAsync<int>(id, existingAppointment, p => p.Id);

            await _IUnitOfWork.Commit();
            return true;
        }

    }
}
