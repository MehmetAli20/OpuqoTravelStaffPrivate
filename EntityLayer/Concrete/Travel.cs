using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Travel
    {
        [Key]   
        public int TravelID { get; set; }
        public string City { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }
        public string Stay { get; set; }
        public string Vehicle { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;

        public int Id { get; set; }
        public Staff Staff { get; set; }
        

        public int StatusID { get; set; }
        public Status Status { get; set; }

        public int? AdminID { get; set; } 
        public Staff Admin { get; set; }

        public virtual List<Message> Messages { get; set; }
    }
}
