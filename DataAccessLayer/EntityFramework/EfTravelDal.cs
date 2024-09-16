using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using EntityLayer.DTOs.MessageDTOs;
using EntityLayer.DTOs.TravelDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfTravelDal : GenericRepository<Travel>, ITravelDal
    {
        public UpdateTravelDto GetByIdUpdate(int travelId)
        {
            using (var c = new Context())
            {
                var travel = c.Travels.Where(x => x.TravelID == travelId).Select(x => new UpdateTravelDto
                {
                    TravelID = x.TravelID,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    City = x.City,
                    Description = x.Description,
                    Stay = x.Stay,
                    Active = x.Active == true ? true : false,
                    Vehicle = x.Vehicle,
                    CreateDate = x.CreateDate,
                    AdminID = x.AdminID.Value,
                }).FirstOrDefault();
                return travel;
            }
        }

        public TravelMessageLayoutDto GetDetailsByTravelId(int travelId)
        {
            using (var c = new Context())
            {
                var travelDetails = c.Travels.Where(x => x.TravelID == travelId && x.Active == true).Select(x => new GetTravelDto
                {
                    TravelID = x.TravelID,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    City = x.City,
                    Description = x.Description,
                    Active = x.Active,
                    Stay = x.Stay,
                    Vehicle = x.Vehicle,
                    CreateDate = x.CreateDate,
                    AdminID = x.AdminID.Value,
                    Id = x.Id,
                }).FirstOrDefault();

                var messageList = c.Messages.Where(x => x.TravelID == travelId && x.Active == true).Select(x => new GetMessageDto
                {
                    MessageID = x.MessageID,
                    Content = x.Content,
                    SendDate = x.SendDate,
                    TravelID = x.TravelID,
                    FromAdmin = x.FromAdmin,
                    Active = x.Active
                    //belki createTravelDto setleyip viewda list döndürmeye çözüm bulunabilir ?
                }).ToList();

                return new TravelMessageLayoutDto
                {
                    getMessageDto = messageList,
                    getTravelDto = travelDetails,
                };

                //var result = c.Messages.Include("Travel").Where(x => x.TravelID == travelId).ToList();
                //TravelMessageLayoutDto test = new TravelMessageLayoutDto();
                //test.getMessageDto = new List<GetMessageDto>();
                //test.getTravelDto = new GetTravelDto
                //{
                //    TravelID = result..Travel.TravelID,
                //    StartDate = x.Travel.StartDate,
                //    EndDate = x.Travel.EndDate,
                //    City = x.Travel.City,
                //    Description = x.Travel.Description,
                //    Active = x.Travel.Active,
                //    Stay = x.Travel.Stay,
                //    Vehicle = x.Travel.Vehicle,
                //    CreateDate = x.Travel.CreateDate,
                //    AdminID = x.Travel.AdminID.Value,
                //    Id = x.Travel.Id,
                //}
            }
        }

        public TravelMessageLayoutDto GetMessageByTravelId(int id)
        {
            using (var c = new Context())
            {
                return c.Messages.Include("Travel").Where(x => x.TravelID == id && x.Active == true).Select(x => new TravelMessageLayoutDto
                {
                    getMessage1Dto = new GetMessageDto
                    {
                        MessageID = x.MessageID,
                        Content = x.Content,
                        SendDate = x.SendDate,
                        TravelID = x.TravelID,
                        FromAdmin = x.FromAdmin,
                        Active = x.Active
                    },
                }).FirstOrDefault();
            }
        }

        public List<GetTravelDto> GetPastTravelsByUserId(int userId)
        {
            using (var c = new Context())
            {
                return c.Travels.Where(x => x.Id == userId && x.Active == false).Select(x => new GetTravelDto
                {
                    TravelID = x.TravelID,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    City = x.City,
                    Description = x.Description,
                    Active = x.Active,
                    Stay = x.Stay,
                    Vehicle = x.Vehicle,
                    CreateDate = x.CreateDate,
                    AdminID = x.AdminID.Value,
                    Id = x.Id,
                }).ToList();
            }
        }

        public List<Travel> GetStaffsTravels(int id)
        {
            using (var c = new Context())
            {
                return c.Travels.Include("Staff").Where(x => x.Id == id && x.Active == true).ToList();
            }
        }

        public void TAddTravel(CreateTravelDto travelEntity)
        {
            using (var c = new Context())
            {
                var addTravel = new Travel
                {
                    StartDate = travelEntity.StartDate,
                    EndDate = travelEntity.EndDate,
                    City = travelEntity.City,
                    Description = travelEntity.Description,
                    Active = true,
                    Stay = travelEntity.Stay,
                    Vehicle = travelEntity.Vehicle,
                    CreateDate = DateTime.Now,
                    AdminID = travelEntity.AdminID,
                    Id = travelEntity.Id,
                    StatusID = 1
                };
                c.Travels.Add(addTravel);
                c.SaveChanges();
            }
        }
    }
}
