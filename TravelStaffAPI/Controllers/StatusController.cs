using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.StatusDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaffAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        IStatusService _IStatusService;
        public StatusController(IStatusService statusService)
        {
            _IStatusService = statusService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var status = _IStatusService.TGetAll();
            return Ok(status);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var status = _IStatusService.TGetById(id);
            return Ok(status);
        }

        [HttpPost("create")]
        public IActionResult Create(CreateStatusDto status)
        {
            if (ModelState.IsValid)
            {
                _IStatusService.TAdd(new Status { Information = status.Information });
                return StatusCode(StatusCodes.Status201Created);
            }
            
            return StatusCode(StatusCodes.Status400BadRequest);
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(int id, UpdateStatusDto status)
        {
            var existingStatus = _IStatusService.TGetById(id);
            if (existingStatus == null)
            {
                return NotFound("There is no such status, wrong ID.");
            }
            existingStatus.Information = status.Information;
            _IStatusService.TUpdate(existingStatus);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var statuss = _IStatusService.TGetById(id);
            if (statuss == null)
            {
                return NotFound("Status Couldn't Found!");
            }
            _IStatusService.TDelete(statuss);

            return Ok("Status Deleted Succesfully!");
        }
    }
}
