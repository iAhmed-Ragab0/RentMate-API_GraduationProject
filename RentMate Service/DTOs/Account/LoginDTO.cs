using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentMate_Service.DTOs.Account
{
    public class LoginDTO
    {
        public string Email { set; get; }
        public string Password { set; get; }
    }
}
