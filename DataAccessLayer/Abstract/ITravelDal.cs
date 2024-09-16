using EntityLayer.Concrete;
using EntityLayer.DTOs.TravelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface ITravelDal : IGenericDal<Travel>
    {
        public List<Travel> GetStaffsTravels(int id);
        public void TAddTravel(CreateTravelDto travelEntity);
        public TravelMessageLayoutDto GetDetailsByTravelId(int travelId);
        public List<GetTravelDto> GetPastTravelsByUserId(int userId);
        public UpdateTravelDto GetByIdUpdate(int travelId);
    }
}
