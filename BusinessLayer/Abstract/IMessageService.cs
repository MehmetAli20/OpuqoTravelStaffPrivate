using EntityLayer.Concrete;
using EntityLayer.DTOs.MessageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IMessageService : IGenericService<Message>
    {
        public List<GetMessageDto> GetMessagesByTravelId(int travelId);
		public void TAddMessage(CreateMessageDto createMessageDto);
	}
}
