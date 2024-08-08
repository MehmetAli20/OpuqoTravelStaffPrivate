using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaffAPI.Controllers
{
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

        [HttpGet("getbyid")]
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

        [HttpPut("update")]
        public IActionResult Update(Status status)
        {
            _IStatusService.TUpdate(status);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Status status)
        {
            _IStatusService.TDelete(status);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
