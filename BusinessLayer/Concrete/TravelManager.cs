using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.DTOs.TravelDTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class TravelManager : ITravelService
    {
        ITravelDal _ITravelDal;

        public TravelManager(ITravelDal iTravelDal)
        {
            _ITravelDal = iTravelDal;
        }

        public TravelMessageLayoutDto GetDetailsByTravelId(int travelId)
        {
           return  _ITravelDal.GetDetailsByTravelId(travelId);
        }

        public List<GetTravelDto> GetPastTravelsByUserId(int userId)
        {
            return _ITravelDal.GetPastTravelsByUserId(userId);
        }

        public void TAdd(Travel t)
        {
            _ITravelDal.Insert(t);     
        }

        public void TAddTravel(CreateTravelDto travelEntity)
        {
            _ITravelDal.TAddTravel(travelEntity);
        }

        public void TDelete(Travel t)
        {
            _ITravelDal.Delete(t);
        }

        public List<Travel> TGetAll()
        {
           return _ITravelDal.GetAll();
        }

        public Travel TGetById(int id)
        {
            return _ITravelDal.GetById(id);
        }

        public UpdateTravelDto TGetByIdUpdate(int travelId)
        {
            return _ITravelDal.GetByIdUpdate(travelId);
        }

        public List<Travel> TGetStaffsTravels(int id)
        {
            return _ITravelDal.GetStaffsTravels(id);
        }
        

        public void TUpdate(Travel t)
        {
            _ITravelDal.Update(t);
        }
    }
}
