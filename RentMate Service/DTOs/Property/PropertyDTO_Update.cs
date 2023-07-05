using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.DTOs.Property
{
    public class PropertyDTO_Update
    {

        public int Id { get; set; }
        public string Title { get; set; }


        public string Description { get; set; }
        public int NoOfBedsPerApartment { get; set; }
        public int NoOfBedsInTheRoom { get; set; }
        public bool IsRented { get; set; }


        //details
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



    }
}
