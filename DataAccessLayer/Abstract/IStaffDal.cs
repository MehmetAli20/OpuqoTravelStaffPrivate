using EntityLayer.Concrete;
using EntityLayer.DTOs.StaffDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Abstract
{
    public interface IStaffDal : IGenericDal<Staff>
    {
        public List<Staff> GetStaffsTravels();
        public Staff GetStaffById(int id);
        public List<GetStaffDto> GetStaffByAdminId(int adminId);
        public Staff GetByCredentials(string username, string password);
    }
}
