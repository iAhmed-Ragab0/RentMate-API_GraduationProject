using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RentMate_Domain.Constant.Enums;

namespace RentMate_Service.DTOs.User
{
    public class UserPropertiesDTO_Getall
    {
        public int PropertyId{ get; set; }
        public string mainPhotoUrl { get; set; }
        public string PropertyTitle { get; set; }
        public decimal PropertyPrice { get; set; }
        public PropertyStatus Status { get; set; }


    }
}
