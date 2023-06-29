using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.DTOs.Reviews
{
    public  class ReviewDTO_GetAll
    {
        public int ReviewId { get; set; }
        public int PropertyId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public string UserId { get; set; }
        //user name 
        //public string UserId { get; set; }

    }
}
