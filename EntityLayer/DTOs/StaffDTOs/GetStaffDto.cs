using EntityLayer.Concrete;
using EntityLayer.DTOs.TravelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.DTOs.StaffDTOs
{
    public class GetStaffDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsAdmin { get; set; }
        public int AdminID { get; set; }
      //  public List<GetTravelDto> Travels { get; set; }
    }
}
