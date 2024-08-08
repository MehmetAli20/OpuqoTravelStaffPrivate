using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Admin
    {
        [Key]
        public int AdminID { get; set; }
        public bool Active { get; set; }

        [ForeignKey("StaffID")]
        public int StaffID { get; set; }
        public Staff Staff { get; set; }

        
        public List<Travel> Travels { get; set; }
        public List<Staff> Staffs { get; set; }
    }
}