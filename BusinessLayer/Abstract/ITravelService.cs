using EntityLayer.Concrete;
using EntityLayer.DTOs.TravelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BusinessLayer.Abstract
{
    public interface ITravelService : IGenericService<Travel>
    {
        void TAddTravel(Travel travelEntity);
        public List<Travel> TGetStaffsTravels(int id);
        //public List<Travel> TGetStaffsTravelsByStaffId(int id);


    }
}
