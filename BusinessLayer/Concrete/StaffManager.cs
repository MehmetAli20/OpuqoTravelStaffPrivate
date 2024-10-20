using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.TravelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class StaffManager : IStaffService
    {
        IStaffDal _IStaffDal;

        public StaffManager(IStaffDal iStaffDal)
        {
            _IStaffDal = iStaffDal;
        }

        public Staff Authenticate(string UserName, string password)
        {
            return _IStaffDal.GetAll().FirstOrDefault(s => s.UserName == UserName );
        }

        public List<GetStaffDto> GetStaffByAdminId(int adminId)
        {
            return _IStaffDal.GetStaffByAdminId(adminId);
            //{
            //    var staffCollection = _IStaffDal.GetAll();

            //    if (staffCollection == null)
            //    {
            //        return new List<GetStaffDto>();
            //    }

            //    var staffList = staffCollection
            //        .Where(s => s.AdminID == adminId)
            //        .Select(s => new GetStaffDto
            //        {
            //            StaffID = s.StaffID,
            //            Name = s.Name,
            //            Surname = s.Surname,
            //            IsAdmin = s.IsAdmin,
            //            AdminID = s.AdminID,
            //            Travels = s.Travels.Select(t => new GetTravelDto
            //            {
            //                TravelID = t.TravelID,
            //                City=t.City,
            //                StartDate = t.StartDate,
            //                EndDate = t.EndDate,
            //                Description = t.Description,
            //                Active = t.Active,
            //                Stay = t.Stay,
            //                Vehicle = t.Vehicle,
            //                CreateDate = t.CreateDate,
            //                StaffID = t.StaffID                          
            //            }).ToList()
            //        })                   
            //        .ToList();

            //    return staffList;
            //}
        }
        
        public List<GetStaffDto> GetStaffs()
        {
            throw new NotImplementedException();
        }

        public void TAdd(Staff t)
        {
            _IStaffDal.Insert(t);
        }

        public void TDelete(Staff t)
        {
            _IStaffDal.Delete(t);
        }

        public List<Staff> TGetAll()
        {
            return _IStaffDal.GetAll();
        }

        public Staff TGetById(int id)
        {
            return _IStaffDal.GetById(id);
        }

        public List<Staff> TGetStaffsTravels()
        {
            return _IStaffDal.GetStaffsTravels();
        }

        public void TUpdate(Staff t)
        {
            _IStaffDal.Update(t);
        }

        public void UpdateStaff(UpdateStaffDto updateStaffDto)
        {
            throw new NotImplementedException();
        }


    }
}
