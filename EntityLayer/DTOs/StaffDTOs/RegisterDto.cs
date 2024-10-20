using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.StaffDTOs
{
        public class RegisterDto
        { 
            public string Name { get; set; }
            public string Surname { get; set; }
            public string UserName { get; set; }
            public string Email { get; set; }                     
            public string Password{ get; set; }
            public string ConfirmPassword { get; set; }
            public bool IsAdmin { get; set; }
            public int? AdminID { get; set; }

    }

        public class RegisterReturnDto
        {
            public bool Success { get; set; }
            //public int Id { get; set; }
            public string ErrorMessage { get; set; }
        }
}
