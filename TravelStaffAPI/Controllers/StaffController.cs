using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.StaffDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaffAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        IStaffService _staffService;
        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var staff = _staffService.TGetAll();
            return Ok(staff);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var staff = _staffService.TGetById(id);
            if (staff == null)
            {
                return NotFound("Staff not found.");
            }
            return Ok(staff);
        }

        [HttpPost("create")]
        public IActionResult Create(CreateStaffDto staff)
        {
            if (ModelState.IsValid)
            {
                _staffService.TAdd(new Staff { Name = staff.Name, Surname = staff.Surname });
                return StatusCode(StatusCodes.Status201Created);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPut("update")]
        public IActionResult Update(UpdateStaffDto staff)
        {
            _staffService.TUpdate(new Staff { Name=staff.Name, Surname=staff.Surname });
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            var staff = _staffService.TGetById(id);
            if (staff == null)
            {
                return NotFound("Staff not found.");
            }
            
            if (!staff.IsAdmin)
            {
                return BadRequest("Only admins can delete staff members.");
            }

            _staffService.TDelete(staff);
            return Ok("Staff deleted successfully.");
        }
    }
}
