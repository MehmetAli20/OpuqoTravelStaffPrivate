using EntityLayer.Concrete;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.TravelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IStaffService : IGenericService<Staff>
    {
        List<GetStaffDto> GetStaffs();
        void UpdateStaff(UpdateStaffDto updateStaffDto);
        public List<Staff> TGetStaffsTravels();
        public List<GetStaffDto> GetStaffByAdminId(int adminId);
        public Staff Authenticate(string name, string password);
    }
}
