using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Domain.Models
{
    public class Governorate
    {
        public int Id { get; set; }
        public string governorate_name_ar { get; set; }
        public string governorate_name_en { get; set; }

        //public ICollection<Propertyy> Properties { get; set; }
        public ICollection<City> Cities { get; set; }


    }
}
