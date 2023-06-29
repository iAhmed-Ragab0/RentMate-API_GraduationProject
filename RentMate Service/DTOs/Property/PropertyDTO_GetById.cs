using RentMate_Domain.Models;
using RentMate_Service.DTOs.Photo;
using RentMate_Service.DTOs.Reviews;
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
        public string OwnerAddress { get; set; }

        //proprty
        public int? Id { get; set; }
        public string Title { get; set; }//
        public int NoOfBedsPerApartment { get; set; }
        public int NoOfBedsInTheRoom { get; set; }
        public int NoOfRooms { get; set; }
        public int NoOfBathroom { get; set; }
        public int AppartmentArea { get; set; }
        public int FloorNumber { get; set; }//

        //public bool Furnishing { get; set; }

        public string City { get; set; }
        public string Governorate { get; set; }
        public string Description { get; set; }//
        public string Street { get; set; }
      
       
        public Property_Type PropertyType { get; set; }
        public bool IsRented { get; set; }
        public decimal PropertyPrice { get; set; }//

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
        public bool? hasElevator { get; set; }



        //reviews & pics
        public double AverageRating { get; set; }
        public List<string> Photos { get; set; } 
        public virtual ICollection<ReviewDTO_GetAll> Reviews { get; set; } = new List<ReviewDTO_GetAll>();

    }
}
