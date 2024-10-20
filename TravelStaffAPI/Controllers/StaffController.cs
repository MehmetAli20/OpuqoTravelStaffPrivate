using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.StatusDTOs;
using EntityLayer.StaffDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TravelStaffAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;
        public IMapper _mapper;

        public StaffController(IStaffService staffService, IMapper mapper)
        {
            _staffService = staffService;
            _mapper = mapper;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var staff = _mapper.Map<List<GetStaffDto>>(_staffService.TGetStaffsTravels());  
            return Ok(staff);
        }
        
        [HttpGet("getstaffbyadminid/{userId}")]
        public IActionResult GetStaffByAdminId(int userId)
        {
            var staffEntities = _staffService.GetStaffByAdminId(userId);
            var staffDtos = _mapper.Map<List<GetStaffDto>>(staffEntities);
            return Ok(staffDtos);
        }

        [HttpGet("getbyid/{id}")]
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
                _staffService.TAdd(new Staff
                {
                    Name = staff.Name,
                    Surname = staff.Surname,
                    //Password = staff.Password,
                    // IsAdmin = staff.IsAdmin,
                    //AdminID = staff.AdminID
                });
                return StatusCode(StatusCodes.Status201Created);
            }

            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPut("update")]
        public IActionResult Update(int id, UpdateStaffDto staff)
        {
            var existingStaff = _staffService.TGetById(id);
            if (existingStaff == null)
            {
                return NotFound("There is no such staff, wrong ID.");
            }
            existingStaff.IsAdmin = staff.IsAdmin;
            _staffService.TUpdate(existingStaff);
            return StatusCode(StatusCodes.Status200OK);
        }

        //DELETE KISMINI SADECE ADMINLER SILECEK SEKILDE AYARLADIM CUNKU GIRIS YAPTIKTAN SONRA CALISIYOR MU KONTROL EDECEGIM.
        [HttpDelete("delete/{id}")]
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
