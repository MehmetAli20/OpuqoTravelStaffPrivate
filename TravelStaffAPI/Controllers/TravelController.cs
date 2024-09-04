using AutoMapper;
using BusinessLayer.Abstract;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using EntityLayer.DTOs.TravelDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaffAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelController : ControllerBase
    {
        ITravelService _travelService;
        public IMapper _mapper;

        public TravelController(ITravelService travelService,IMapper mapper)
        {
            _travelService = travelService;
            _mapper = mapper;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var travels = _mapper.Map<List<GetTravelDto>>(_travelService.TGetAll());
            return Ok(travels);
          
        }

        [HttpGet("getstaffstravels/{id}")]
        public IActionResult GetStaffsTravels(int id)
        {
            var staffsTravels = _mapper.Map<List<GetTravelDto>>(_travelService.TGetStaffsTravels(id));
            return Ok(staffsTravels);
        }

        //APİ'ye verilen değişkenileri ui'da aynı yap.
        //APİ'ye verilen değişkenileri ui'da aynı yap. AllowAnonymous engel oluyor
        //APİ'ye verilen değişkenileri ui'da aynı yap. AllowAnonymous engel oluyor
        //APİ'ye verilen değişkenileri ui'da aynı yap. AllowAnonymous engel oluyor AllowAnonymous engel oluyor
        //APİ'ye verilen değişkenileri ui'da aynı yap. AllowAnonymous engel oluyor AllowAnonymous engel oluyor AllowAnonymous engel oluyor
        //APİ'ye verilen değişkenileri ui'da aynı yap. AllowAnonymous engel oluyor AllowAnonymous engel oluyor AllowAnonymous engel oluyor
        //APİ'ye verilen değişkenileri ui'da aynı yap. AllowAnonymous engel oluyor AllowAnonymous engel oluyor
        //APİ'ye verilen değişkenileri ui'da aynı yap. AllowAnonymous engel oluyor
        //APİ'ye verilen değişkenileri ui'da aynı yap. AllowAnonymous engel oluyor
        //APİ'ye verilen değişkenileri ui'da aynı yap.
        [HttpGet("getbyid/{id}")]
        public IActionResult GetById(int id)
        {
            var travel = _travelService.TGetById(id);
            return Ok(travel);
        }
        //session ile iki kez aynı veri set olur mu, userId için.
        [HttpPost("create")]
        public IActionResult Create(CreateTravelDto createTravelDto)
        {
            if (ModelState.IsValid)
            {
                var travelEntity = _mapper.Map<Travel>(createTravelDto);
                _travelService.TAddTravel(travelEntity);
                var createdTravelDto = _mapper.Map<CreateTravelDto>(travelEntity);
                return StatusCode(StatusCodes.Status201Created, createdTravelDto);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        
        [HttpPut("update/{id}")]
        public IActionResult Update(int id, UpdateTravelDto updateTravelDto)
        {
            if (ModelState.IsValid)
            {
                var existingTravel = _travelService.TGetById(id);
                if (existingTravel == null)
                {
                    return NotFound();
                }
                _mapper.Map(updateTravelDto, existingTravel);
                _travelService.TUpdate(existingTravel);
                return Ok(existingTravel);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var travel = _travelService.TGetById(id);
            if (travel == null)
            {
                return NotFound("Travel Couldn't Found!");
            }

            // Seyahati sil
            _travelService.TDelete(travel);

            return Ok("Travel Deleted Succesfully!");
        }
    }
}
