using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Domain.Models
{
    public class PropertyDetails
    {
        public int Id { get; set; }
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


        //[ForeignKey("Property")]
        public int? PropertyId { get; set; }


        //navigation properties
        public virtual Propertyy Property { get; set; } 
    }
}
