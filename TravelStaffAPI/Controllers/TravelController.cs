using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.TravelDTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaffAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelController : ControllerBase
    {
        ITravelService _travelService;

        public TravelController(ITravelService travelService)
        {
            _travelService = travelService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var travels = _travelService.TGetAll();
            return Ok(travels);
          
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var travel = _travelService.TGetById(id);
            return Ok(travel);
        }

        [HttpPost("create")]
        public IActionResult Create(CreateTravelDto travel)
        {
            _travelService.TAdd(new Travel { City = travel.City, StartDate = travel.StartDate, EndDate = travel.EndDate,Description=travel.Description, Stay = travel.Stay, Vehicle = travel.Vehicle, StaffID = travel.StaffID , StatusID = travel.StatusID });
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("update")]
        public IActionResult Update(Travel travel)
        {
            _travelService.TUpdate(travel);
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpDelete("delete")]
        public IActionResult Delete(Travel travel)
        {
            _travelService.TDelete(travel);
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
