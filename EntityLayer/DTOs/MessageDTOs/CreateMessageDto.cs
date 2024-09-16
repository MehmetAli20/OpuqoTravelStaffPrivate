using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.MessageDTOs
{
    public class CreateMessageDto
    {
        public string Content { get; set; }
        public int TravelID { get; set; }
        public bool FromAdmin { get; set; }
        public bool Active { get; set; }
        public DateTime SendDate { get; set; } = DateTime.Now;
        public int UserID { get; set; }
    }
}
