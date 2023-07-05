using RentMate_Service.DTOs.Photo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RentMate_Domain.Constant.Enums;

namespace RentMate_Service.DTOs.Property
{
    public class PropertyDTO_Add
    {

        public string OwnerId { get; set; }
        public string Title { get; set; }
        public Property_Type PropertyType { get; set; }
        public decimal PropertyPrice { get; set; }

        public int CityId { get; set; }
        public string StreetDetails { get; set; }

        public string Description { get; set; }
        public int NoOfBedsPerApartment { get; set; }
        public int NoOfBedsInTheRoom { get; set; }
        public int NoOfRooms { get; set; }
        public int NoOfBathroom { get; set; }
        public int AppartmentArea { get; set; }
        public int FloorNumber { get; set; }//




        //public virtual ICollection<PhotoDTO_Add>? Photos { get; set; }


        // property Details
        public bool? hasKitchen { get; set; } 
        public bool? hasAirConditioner { get; set; } 
        public bool? hasMicrowave { get; set; } 
        public bool? hasDishWasher { get; set; } 
        public bool? hasWifi { get; set; } 
        public bool? hasRefrigerator { get; set; } 
        public bool? hasDishesAndSilverware { get; set; } 
        public bool? hasParking { get; set; } 
        public bool? hasWaterHeater { get; set; }
        public bool? hasElivator { get; set; }

    }
}
