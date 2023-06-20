using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentMate_Domain.Models;
using RentMate_Service.DTOs.Appointment;
using RentMate_Service.DTOs.Property;
using RentMate_Service.IServices;
using RentMate_Service.Services;

namespace RentMate_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly IAppointmentService _AppointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _AppointmentService = appointmentService;
        }

        [HttpGet("OwnerAppointments/{owner}")]
        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentOwner([FromRoute] string owner)
        {

            try
            {
                var owners = await _AppointmentService.GetAllAppointmentForOwner(owner);

                return Ok(owners);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("TenantAppointments/{tenant}")]

        public async Task<ActionResult<IEnumerable<Appointment>>> GetAppointmentForTenant([FromRoute] string tenant)
        {
            try
            {
                var TenantSppointments = await _AppointmentService.GetAllAppointmentForTenant(tenant);

                return Ok(TenantSppointments);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddAppointment(AppointmentDTO appointmentDto)
        {

            try
            {
                await _AppointmentService.AddAppointment(appointmentDto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> DeletePropertyById(int id)
        {
            var result = await _AppointmentService.DeleteAppointmentByIdAsync(id);

            if (result != null)
                return Ok($"Property with id =  {id} is Deleted Succesfully");
            else
                return BadRequest();
        }

        [HttpPut]
        public async Task<ActionResult> UpdatePropertyById(int id, AppointmentDTO_Update property)
        {
            var Result = _AppointmentService.UpdateAppointmentByIdAsync(id, property);

            if (Result != null)
                return Ok($"Property with id =  {id} is Updated Succesfully");
            else
                return BadRequest();
        }

    }
}
