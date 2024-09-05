using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.MessageDTOs
{
    public class GetMessageDto
    {
        public int MessageID { get; set; }
        public string Content { get; set; }
    }
}
