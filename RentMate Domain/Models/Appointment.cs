using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RentMate_Domain.Constant.Enums;

namespace RentMate_Domain.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime TourDay { get; set; }
        public int TourHour { get; set; }
        public string Message { get; set; }
        public ReservationStatus reservationstatus { set; get; } = ReservationStatus.Pending;


        //Forign Keys
        [ForeignKey("Owner")]
        public string OwnerId { get; set; }

        [ForeignKey("Tenant")]
        public string TenantId { get; set; }

        [ForeignKey("Property")]
        public int PropertyId { get; set; }


        //nav property
        public virtual User Owner { get; set; }
        public virtual User Tenant { get; set; }
        public virtual Propertyy Property { get; set; }


    }
}
