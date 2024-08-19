using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using EntityLayer.TravelDTOs;
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

        public List<Travel> GetStaffsTravels()
        {
            using (var c = new Context())
            {
                return c.Travels.Include("Staff").DefaultIfEmpty().ToList();
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
