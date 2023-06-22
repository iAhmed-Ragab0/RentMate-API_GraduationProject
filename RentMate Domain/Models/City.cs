using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Domain.Models
{
    public class City
    {
        public int Id { get; set; }
        public string city_name_ar { get; set; }
        public string city_name_en { get; set; }
        public int governorate_id { get; set; }

        public ICollection<Propertyy> Properties { get; set; }
        public Governorate Governorate { get; set; }


    }
}
