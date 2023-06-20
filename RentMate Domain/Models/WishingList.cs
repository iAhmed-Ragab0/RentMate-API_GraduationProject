using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Domain.Models
{
    public class WishingList
    {
        public int Id { get; set; }


        //Forign Keys
        [ForeignKey("Property")]
        public int PropertyId { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }


        //navigation properties
        public virtual Propertyy Property { get; set; }
        public virtual User User { get; set; }

    }
}
