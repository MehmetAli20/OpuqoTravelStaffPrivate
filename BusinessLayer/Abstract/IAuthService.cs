using EntityLayer.Concrete;

namespace BusinessLayer.Abstract
{
    public interface IAuthService
    {
        Staff Login(string username, string password);
    }
}
