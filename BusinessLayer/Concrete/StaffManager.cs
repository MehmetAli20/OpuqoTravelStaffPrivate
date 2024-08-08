using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
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

        public void TUpdate(Staff t)
        {
            _IStaffDal.Update(t);
        }
    } 
}
