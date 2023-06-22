using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RentMate_Domain.Constant.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace RentMate_Domain.Models
{
    public  class Propertyy
    {

        public int Id { get; set; }
        public string Title { get; set; }

        [EnumDataType(typeof(Property_Type), ErrorMessage = "Invalid Type. Only 'Apartment', 'Room' and 'Bed' are allowed.")]
        public Property_Type PropertyType { get; set; }
        public decimal PropertyPrice { get; set; }

        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public string Description { get; set; }
        public int NoOfRooms { get; set; }
        public int NoOfBedsInTheRoom { get; set; }
        public int NoOfBedsPerApartment { get; set; }
        public bool IsRented { get; set; }


        // Payment Table
        public string StripeId { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }


        //FK for the tables

        [ForeignKey("Owner")]

        public string OwnerId { get; set; }

        [ForeignKey("Tenant")]
        public string? TenantId { get; set; }

        [ForeignKey("Details")]
        public int? DetailsId { get; set; }


        //nav property
        public virtual User Owner { get; set; }
        public virtual User Tenant { get; set; }
        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual PropertyDetails Details { get; set; } = new PropertyDetails();


    }
}
