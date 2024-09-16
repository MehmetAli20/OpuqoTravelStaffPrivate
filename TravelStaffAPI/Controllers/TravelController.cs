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
    [Authorize]
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

        [HttpGet("getstaffstravels/{userId}")]
        public IActionResult GetStaffsTravels(int userId)
        {
            var staffsTravels = _mapper.Map<List<GetTravelDto>>(_travelService.TGetStaffsTravels(userId));
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

        
        // BURADAN HATA ALABİLİRSİN DİKAKT ******************
        [HttpGet("getbyid/{travelId}")]
        public IActionResult GetById(int travelId)
        {
            var travel = _travelService.GetDetailsByTravelId(travelId);
            return Ok(travel);
        }

        [HttpGet("getbyidupdate/{travelId}")]
        public IActionResult GetByIdUpdate(int travelId)
        {
            var travel = _travelService.TGetByIdUpdate(travelId);
            return Ok(travel);
        }

        //session ile iki kez aynı veri set olur mu, userId için.       
        [HttpPost("create")]
        public IActionResult Create(CreateTravelDto createTravelDto)
        {
            if (ModelState.IsValid)
            {
                var travelEntity = _mapper.Map<CreateTravelDto>(createTravelDto);
                _travelService.TAddTravel(travelEntity);
                var createdTravelDto = _mapper.Map<CreateTravelDto>(travelEntity);
                return StatusCode(StatusCodes.Status201Created, createdTravelDto);
            }
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPut("update/{travelId}")]
        public IActionResult Edit(int travelId, [FromBody] UpdateTravelDto updateTravelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingTravel = _travelService.TGetById(travelId);
            if (existingTravel == null)
            {
                return NotFound();
            }

            // Veriyi DTO'dan mevcut varlığa haritalandır
            _mapper.Map(updateTravelDto, existingTravel);

            try
            {
                _travelService.TUpdate(existingTravel);
            }
            catch (Exception ex)
            {
                // Loglama yapılabilir veya daha anlamlı bir hata mesajı dönebilir
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error updating travel: {ex.Message}");
            }

            return Ok(existingTravel);
        }

        //{
        //    if (ModelState.IsValid)
        //    {
        //        var existingTravel = _travelService.TGetById(travelId);
        //        if (existingTravel == null)
        //        {
        //            return NotFound();
        //        }
        //        _mapper.Map(updateTravelDto, existingTravel);
        //        _travelService.TUpdate(existingTravel);
        //        return Ok(existingTravel);
        //    }
        //    return StatusCode(StatusCodes.Status500InternalServerError);
        //}

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var travel = _travelService.TGetById(id);
            if (travel == null)
            {
                return NotFound("Travel Couldn't Found!");
            }
            travel.Active = false;
            _travelService.TUpdate(travel);
            
            //_travelService.TDelete(travel);

            return Ok("Travel is not active anymore!");
        }

        [HttpGet("getpasttravelsbyuserid/{userId}")]
        public IActionResult GetPastTravelsByUserId(int userId)
        {
            var pastTravels = _mapper.Map<List<GetTravelDto>>(_travelService.GetPastTravelsByUserId(userId));
            return Ok(pastTravels);
        }


    }
}
