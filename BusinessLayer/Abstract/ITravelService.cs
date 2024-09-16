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
        public List<GetTravelDto> GetPastTravelsByUserId(int userId);
        public void TAddTravel(CreateTravelDto travelEntity);
        public List<Travel> TGetStaffsTravels(int id);
        public TravelMessageLayoutDto GetDetailsByTravelId(int travelId);
        public UpdateTravelDto TGetByIdUpdate(int travelId);
        //public List<Travel> TGetStaffsTravelsByStaffId(int id);


    }
}
