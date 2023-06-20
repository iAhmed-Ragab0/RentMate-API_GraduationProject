using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Domain.Models
{
    public class Review
    {
        public int Id { get; set; }
        public int PropertyId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }


        //nav property
        public virtual User User { get; set; }

    }
}
