using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.DTOs.PropertyDetails
{
    public class PropertyDetailsDTO
    {
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
