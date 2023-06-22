using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Domain.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }


         //[ForeignKey("Property")]
        public int PropertyId { get; set; }


        // add nav prop of the Property table
        // public virtual Property Property { get; set; }

    }
}
