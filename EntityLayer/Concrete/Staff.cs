using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EntityLayer.Concrete
{
    public class Staff : IdentityUser<int>
    {
       // public int StaffID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        //public string Password { get; set; }
        public bool Active { get; set; }
        public bool IsAdmin { get; set; }

        public int? AdminID { get; set; } 
        public Staff Admin { get; set; }
  
        public virtual List<Travel> Travels { get; set; }

        //public virtual List<Staff> Staffs { get; set; }
        //public virtual List<Travel> TravelAdmins { get; set; }
        
    }
}
