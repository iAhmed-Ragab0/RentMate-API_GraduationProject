using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Domain.Models
{
    public class PropertyDetails
    {
        public int Id { get; set; }
        public bool? hasKitchen { get; set; } = true;
        public bool? hasAirConditioner { get; set; } = false;
        public bool? hasMicrowave { get; set; } = false; 
        public bool? hasDishWasher { get; set; } = false;
        public bool? hasWifi { get; set; } = false;
        public bool? hasRefrigerator { get; set; } = false;
        public bool? hasDishesAndSilverware { get; set; } = false;  
        public bool? hasParking { get; set; } = false;
        public bool? hasWaterHeater { get; set; } = false;

        [ForeignKey("Property")]
        public int PropertyId { get; set; }


        //navigation properties
        public virtual Propertyy Property { get; set; }
    }
}
