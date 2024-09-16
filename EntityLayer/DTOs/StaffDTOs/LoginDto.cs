using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.StaffDTOs
{
    public class LoginDto
    {
        //public string Name { get; set; }
        public string UserName { get; set; }
        public int AdminID { get; set; }
        public string Password { get; set; }
        //public string Token { get; set; }

        // public string Pw { get; set; }
    }

    public class LoginReturnDto
    {
        public int Id { get; set; }
        //public string RedirectUrl { get; set; }
        public bool Success { get; set; }
        public bool IsAdmin { get; set; }
        public string Message { get; set; }
        public int AdminID { get; set; } 
        public string Token { get; set; }
    }
}
