using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using EntityLayer.DTOs.MessageDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfMessageDal : GenericRepository<Message>, IMessageDal
    {
        public List<GetMessageDto> GetMessagesByTravelId(int travelId)
        {
            using (var context = new Context())
            {
                return context.Messages.Where(m => m.TravelID == travelId && m.Active== true).OrderBy(m => m.TravelID).Select(m => new GetMessageDto
                {
                    MessageID = m.MessageID,
                    Content = m.Content,
                    SendDate = m.SendDate,
                    FromAdmin = m.FromAdmin,
                    TravelID = m.TravelID,
                    Active = m.Active,
                    //createMessageDto = new CreateMessageDto
                    //{
                    //    Content = m.Content,
                    //    TravelID = m.TravelID,
                    //    FromAdmin = m.FromAdmin,
                    //    Active = m.Active
                    //}
                }).ToList();
            }
        }

        public void TAddMessage(CreateMessageDto createMessageDto)
        {
            var message = new Message
            {
                Content = createMessageDto.Content,
                TravelID = createMessageDto.TravelID,
                SendDate = DateTime.Now,
                FromAdmin = createMessageDto.FromAdmin,
                Active = true  //ileride fonksiyon ile butona eklenirse buradan createMessageDto.Active ile değiştirilebilir
            };
            Insert(message); // GenericRepository içindeki Insert fonksiyonunu kullanıyoruz
        }
    }
}
