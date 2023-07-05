using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Domain.Models
{
    public class User : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string Address { get; set; }
        public string? ProfileImg { get; set; }
        public bool IsDeleted { get; set; }


        //nav property
        public virtual ICollection<Propertyy> OwenedPoperty { get; set; }
        public virtual ICollection<Propertyy> RentedProperty { get; set; }
        public virtual ICollection<Appointment> appointmentOfOwner { get; set; }
        public virtual ICollection<Appointment> appointmentsOfTenant { get; set; }
        public virtual ICollection<WishingList> WishingList { get; set; }


        //override identity columns
        /* public int ? AccessFailedCount { get; set; }
         public bool ? PhoneNumberConfirmed { get; set; }
         public bool ? TwoFactorEnabled { get; set; }
         public bool ? EmailConfirmed { get; set; }
         public bool ? LockoutEnabled { get; set; }
        */


    }
}
