using EntityLayer.Concrete;
using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

//namespace TravelStaffAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthController : ControllerBase
//    {
//        Staff _authService;
//        public AuthController(Staff authService)
//        {
//            _authService = authService;
//        }

//        [HttpPost("login")]
//        public IActionResult Login(Staff staff)
//        {
//            var user = _authService.TLogin(staff); // _authService ve TLogin yerine doğru metodu yazılacak

//            if (user != null)
//            {
//                return Ok(user);
//            }
//            return StatusCode(StatusCodes.Status401Unauthorized);
//        }
//    }
//}
