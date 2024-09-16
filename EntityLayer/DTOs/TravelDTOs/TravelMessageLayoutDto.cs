using EntityLayer.DTOs.MessageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.TravelDTOs
{
	public class TravelMessageLayoutDto
	{
        public GetTravelDto? getTravelDto { get; set; }
        public List<GetMessageDto> getMessageDto { get; set; }
        public GetMessageDto? getMessage1Dto { get; set; }
        public CreateMessageDto? createMessageDto { get; set; }
        public UpdateTravelDto? updateTravelDto { get; set; }
    }
}
