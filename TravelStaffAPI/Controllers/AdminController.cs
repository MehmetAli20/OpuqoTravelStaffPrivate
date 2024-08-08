using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaffAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IAdminService _IAdminService;
        public AdminController(IAdminService adminService)
        {
            _IAdminService = adminService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var admins = _IAdminService.TGetAll();
            return Ok(admins);
        }

        [HttpPost("create")]

        public IActionResult Create(Admin admin)
        {
            _IAdminService.TAdd(admin);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("update")]
        public IActionResult Update(Admin admin)
        {
            _IAdminService.TUpdate(admin);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Admin admin)
        {
            _IAdminService.TDelete(admin);
            return StatusCode(StatusCodes.Status200OK);
        }


    }
}
