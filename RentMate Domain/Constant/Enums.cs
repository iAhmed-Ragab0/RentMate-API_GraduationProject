using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Domain.Constant
{
    public class Enums
    {
        public enum Gender 
        {
            Male = 1, 
            Female = 2
        }

        public enum ReservationStatus
        {
            Rejected = 0,
            Approved = 1,
            Pending = 2
        }

        public enum PropertyStatus
        {
            Pending = 0,
            Approved = 1
        }

        public enum Property_Type
        {
            Apartment = 1,
            Room = 2,
            Bed = 3,
        }
    }
}
