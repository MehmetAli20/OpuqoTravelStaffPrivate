using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;

namespace BusinessLayer.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IStaffDal _staffDal;

        public AuthManager(IStaffDal staffDal)
        {
            _staffDal = staffDal;
        }

        public Staff Login(string username, string password)
        {
            return _staffDal.GetByCredentials(username, password);
        }
    }
}