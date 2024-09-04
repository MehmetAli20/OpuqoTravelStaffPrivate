using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
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

        public List<Travel> GetStaffsTravels(int id)
        {
            using (var c = new Context())
            {
                return c.Travels.Include("Staff").Where(x=>x.Id==id).ToList();
            }
        }

        public void TAddTravel(Travel travelEntity)
        {
            using (var c = new Context())
            {
                c.Travels.Add(travelEntity);
                c.SaveChanges();
            }
        }
    }
}
