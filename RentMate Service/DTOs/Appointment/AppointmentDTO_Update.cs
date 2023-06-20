using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RentMate_Domain.Constant.Enums;

namespace RentMate_Service.DTOs.Appointment
{
    public class AppointmentDTO_Update
    {
        public int Id { get; set; }

        public ReservationStatus reservationstatus { set; get; } = ReservationStatus.Pending;
    }
}
