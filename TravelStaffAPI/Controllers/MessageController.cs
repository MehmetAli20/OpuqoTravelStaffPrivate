using AutoMapper;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.MessageDTOs;
using EntityLayer.DTOs.TravelDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TravelStaffAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        IMessageService _IMessageService;
        public IMapper _mapper;

        public MessageController(IMessageService messageService, IMapper mapper)
        {
            _IMessageService = messageService;
            _mapper = mapper;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var messages = _mapper.Map<List<GetMessageDto>>(_IMessageService.TGetAll());
            return Ok(messages);
        }

        //TRAVELID KISMI UI TARAFINDAN ALINACAK VE BURADA ATANACAK BELKİ İLERİDE ORTAK SESSİON KURULUP JWT İLE DE YAPILABİLİR ŞİMDİLİK BU ŞEKİLDE YAPILDI
        [HttpGet("getbytravelid/{travelId}")]
        public IActionResult GetMessagesByTravelId(int travelId)
        {
            var messages = _mapper.Map<List<GetMessageDto>>(_IMessageService.GetMessagesByTravelId(travelId));
            return Ok(messages);
        }

        [HttpGet("getbyid/{id}")]
        public IActionResult GetMessageById(int travelId)
        {
            var message = _mapper.Map<GetMessageDto>(_IMessageService.TGetById(travelId));
            return Ok(message);
        }

        //[HttpPost("create")]
        //public IActionResult CreateMessage([FromBody] TravelMessageLayoutDto travelMessageLayoutDto) //TravelLayoutMessageDto ile ilgili travel'i alıp ef'de setleyebilirm ama daha iyi bir çözüm bulup onu yapacağım.
        //{
        //    if (ModelState.IsValid)
        //    {       
        //        _IMessageService.TAddMessage(travelMessageLayoutDto.createMessageDto);
        //        return Ok();
        //    }
        //    return BadRequest("Invalid Data");
        //}

        [HttpPost("create")]
        public IActionResult CreateMessage(CreateMessageDto createMessageDto)
        {
            var info = createMessageDto.FromAdmin;
            if (ModelState.IsValid)
            {             
                var message = new CreateMessageDto
                {
                    TravelID = createMessageDto.TravelID,
                    Content = createMessageDto.Content,
                    SendDate = DateTime.Now,
                    FromAdmin = createMessageDto.FromAdmin, 
                    Active = true
                };

                _IMessageService.TAddMessage(message);
                return Ok();
            }
            return BadRequest("Invalid Data");
        }

        [HttpPut("updatebyid/{id}")]
        public IActionResult UpdateMessage(int id, [FromBody] UpdateMessageDto updateMessageDto)
        {
            
            return Ok();
        }

        [HttpDelete("deletebyid/{id}")]
        public IActionResult DeleteMessage(int id)
        {
            return Ok();
        }
    }
}
