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
    public class Staff
    {
        [Key]
        public int StaffID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool Active { get; set; }
        public virtual List<Travel> Travels { get; set; }
        public bool IsAdmin { get; set; }
               
        //[ForeignKey("AdminID")]
        //public int AdminID { get; set; }
        //public Admin Admin { get; set; }
        
        //public List<Admin> Admins { get; set; }
    }
}
