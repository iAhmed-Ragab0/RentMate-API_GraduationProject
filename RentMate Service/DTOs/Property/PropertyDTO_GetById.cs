using RentMate_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RentMate_Domain.Constant.Enums;

namespace RentMate_Service.DTOs.Property
{
    public class PropertyDTO_GetById
    {
        //owner properties
        public string OwnerId { get; set; }
        public string OwnerProfileImg { get; set; }
        public string OwnerFullName { get; set; }
        public string OwnerPhone { get; set; }
        public string OwnerEmail { get; set; }

        //proprty
        public int? Id { get; set; }
        public string Title { get; set; }
        public int? NoOfBedsPerApartment { get; set; }
        public int? NoOfBedsInTheRoom { get; set; }
        public int? NoOfRooms { get; set; }
        public string City { get; set; }
        public string Governorate { get; set; }
        public string Description { get; set; }
        public string Street { get; set; }
      
        //to do add enum to the type
        public Property_Type PropertyType { get; set; }
        public bool IsRented { get; set; }
        public List<Photo> Photos { get; set; } = new List<Photo> { };
        public decimal PropertyPrice { get; set; }

        //Details
        public bool? hasKitchen { get; set; }
        public bool? hasAirConditioner { get; set; }
        public bool? hasMicrowave { get; set; }
        public bool? hasDishWasher { get; set; }
        public bool? hasWifi { get; set; }
        public bool? hasRefrigerator { get; set; }
        public bool? hasDishesAndSilverware { get; set; }
        public bool? hasParking { get; set; }
        public bool? hasWaterHeater { get; set; }


        //reviews
        public double AverageRating { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
}
