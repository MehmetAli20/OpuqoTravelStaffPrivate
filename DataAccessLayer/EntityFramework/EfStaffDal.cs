using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repository;
using EntityLayer.Concrete;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.TravelDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFramework
{
    public class EfStaffDal : GenericRepository<Staff>, IStaffDal
    {
        public List<GetStaffDto> GetStaffByAdminId(int adminId)
        {
            using (var context = new Context())
            {
                return context.Staffs.Include("Travels").Where(s => s.AdminID == adminId).Select(s => new GetStaffDto
                {
                    StaffID = s.StaffID,
                    Name = s.Name,
                    Surname = s.Surname,
                    IsAdmin = s.IsAdmin,
                    AdminID = s.AdminID,
                    //Travels = s.Travels.Select(t => new GetTravelDto
                    //{
                    //    TravelID = t.TravelID,
                    //    City = t.City,
                    //    StartDate = t.StartDate,
                    //    EndDate = t.EndDate,
                    //    Description = t.Description,
                    //    Active = t.Active,
                    //    Stay = t.Stay,
                    //    Vehicle = t.Vehicle,
                    //    CreateDate = t.CreateDate,
                    //    StaffID = t.StaffID
                    //}).ToList()
                }).ToList();

            }
        }

        public Staff GetStaffById(int id)
        {
            using (var context = new Context())
            {
                return context.Staffs.Include("Travels").FirstOrDefault(x => x.StaffID == id);
            }
        }

        public List<Staff> GetStaffsTravels()
        {
            using (var context = new Context())
            {
                return context.Staffs.Include("Travels").DefaultIfEmpty().ToList();
            }

        }


        public Staff UpdateStaff(int id,Staff staff)
        {
            using (var context = new Context())
            {
                return context.Staffs.Update(staff).Entity;
            }
        }
    }
}
