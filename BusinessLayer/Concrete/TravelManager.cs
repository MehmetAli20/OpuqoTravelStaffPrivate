using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.TravelDTOs;
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

        
        public void TAdd(Travel t)
        {
            _ITravelDal.Insert(t);     
        }

        public void TAddTravel(Travel travelEntity)
        {
            _ITravelDal.Insert(travelEntity);
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
        
        public List<Travel> TGetStaffsTravels()
        {
            return _ITravelDal.GetStaffsTravels();
        }

        public void TUpdate(Travel t)
        {
            _ITravelDal.Update(t);
        }
    }
}
