using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RentMate_Domain.Constant.Enums;

namespace RentMate_Service.DTOs.Property
{
    public class PropertyDTO_GetAll
    {

        //property
        public int Id { get; set; }
        public string Title { get; set; }
        public Property_Type PropertyType { get; set; }
        public decimal PropertyPrice { get; set; }
        public string MainPhotoUrl { get; set; }

        public string Description { get; set; }
        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public bool IsRented { get; set; }


        //calcaulated
        public double AverageRating { get; set; }

        //owner details
        public string OwnerPhoto { get; set; }
        public string OwnerFullName { get; set; }

    }
}
