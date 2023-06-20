using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RentMate_Domain.Constant.Enums;

namespace RentMate_Service.DTOs.Appointment
{
    public class AppointmentDTO
    {
        public DateTime TourDay { get; set; }
        public int TourHour { get; set; }

        public string Message { get; set; }


        ////[ForeignKey("Owner")]
        public string OwnerId { get; set; }

        //[ForeignKey("Tenant")]
        public string TenantId { get; set; }


        //[ForeignKey("Property")]
        public int PropertyId { get; set; }
    }
}
