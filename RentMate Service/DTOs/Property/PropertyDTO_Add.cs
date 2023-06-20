using System;
using System.Collections.Generic;
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

        public string Governorate { get; set; }
        public string City { get; set; }
        public string Street { get; set; }

        public string Description { get; set; }
        public int NoOfBedsPerApartment { get; set; }
        public int NoOfBedsInTheRoom { get; set; }
        public int NoOfRooms { get; set; }
        public bool IsRented { get; set; }

        public string StripeId { get; set; }
        public long Amount { get; set; }

        //public virtual ICollection<Photo> Photos { get; set; }
    }
}
