using EntityLayer.Concrete;
using EntityLayer.TravelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ITravelDal : IGenericDal<Travel>
    {
        public List<Travel> GetStaffsTravels();

        public void TAddTravel(Travel travelEntity);
    }
}
