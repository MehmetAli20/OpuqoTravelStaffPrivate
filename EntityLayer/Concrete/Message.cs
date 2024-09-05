using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class Message
    {
        [Key]
        public int MessageID { get; set; }
        public string Content { get; set; }
        public DateTime SendDate { get; set; }
        public bool FromAdmin { get; set; }
        public bool Active { get; set; }
        
        public int TravelID { get; set; }
        public Travel Travel { get; set; }

    }
}
