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
    public class AdminManager : IAdminService
    {
        IAdminDal _IAdminDal;

        public AdminManager(IAdminDal IAdminDal)
        {
            _IAdminDal = IAdminDal;
        }

        public void TAdd(Admin t)
        {
            _IAdminDal.Insert(t);
        }

        public void TDelete(Admin t)
        {
            _IAdminDal.Delete(t);
        }

        public void TUpdate(Admin t)
        {
            _IAdminDal.Update(t);
        }

        public List<Admin> TGetAll()
        {
            return _IAdminDal.GetAll();
        }

        public Admin TGetById(int id)
        {
            return _IAdminDal.GetById(id);
        }

    }
}
