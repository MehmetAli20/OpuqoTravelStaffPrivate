using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.MessageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {
        IMessageDal _IMessageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _IMessageDal = messageDal;
        }

        public void TAdd(Message t)
        {
            _IMessageDal.Insert(t);
        }
        public void TUpdate(Message t)
        {
            _IMessageDal.Update(t);
        }
        public void TDelete(Message t)
        {
            _IMessageDal.Delete(t);
        }

        public List<Message> TGetAll()
        {
            return _IMessageDal.GetAll();
        }

        public Message TGetById(int id)
        {
            return _IMessageDal.GetById(id);
        }

        public List<GetMessageDto> GetMessagesByTravelId(int travelId)
        {
            return _IMessageDal.GetMessagesByTravelId(travelId);
        }

        public void TAddMessage(CreateMessageDto createMessageDto)
        {
            _IMessageDal.TAddMessage(createMessageDto);
        }
    }
}
