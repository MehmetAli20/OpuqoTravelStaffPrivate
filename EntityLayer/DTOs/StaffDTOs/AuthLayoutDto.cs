using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.StaffDTOs
{
    public class AuthLayoutDto
    {
        public LoginDto? LoginDto { get; set; }
        public LoginReturnDto? LoginReturnDto { get; set; }
        public RegisterDto? RegisterDto { get; set; }
        public RegisterReturnDto? RegisterReturnDto { get; set; }
    }
    
}
