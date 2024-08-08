using Microsoft.AspNetCore.Mvc;

namespace TravelStaffAPI.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
