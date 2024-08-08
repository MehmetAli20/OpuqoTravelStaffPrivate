using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Status
    {
        [Key]
        public int StatusID { get; set; }
        public bool Active { get; set; }
        public string Information { get; set; }
        public List<Travel> Travels { get; set; }
    }
}
