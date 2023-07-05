using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.DTOs.Photo
{
    public class photoDTO_Update
    {
        public int id { get; set; }
        public string Url { get; set; }
        public string PublicId { get; set; }
    }
}
