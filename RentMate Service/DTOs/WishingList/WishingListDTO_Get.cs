using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.DTOs.WishingList
{
    public class WishingListDTO_Get
    {
        public int PropertyId { get; set; }
        public string UserId { get; set; }

        public string PropertyTitle { get; set; }
        public string PropertyPhoto { get; set; }
        public decimal PropertyPrice { get; set; }
    }
}
