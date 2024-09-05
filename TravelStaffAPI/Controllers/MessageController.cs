using AutoMapper;
using BusinessLayer.Abstract;
using EntityLayer.Concrete;
using EntityLayer.DTOs.MessageDTOs;
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

        [HttpGet("getbyid/{id}")]
        public IActionResult GetMessageById(int id)
        {
            var message = _mapper.Map<GetMessageDto>(_IMessageService.TGetById(id));
            return Ok(message);
        }

        [HttpPost("create")]
        public IActionResult CreateMessage([FromBody] CreateMessageDto createMessageDto)
        {
            if (ModelState.IsValid)
            {
                _IMessageService.TAdd(new Message
                {
                    TravelID = 12,
                    Content = createMessageDto.Content,
                    SendDate = DateTime.Now,
                    FromAdmin = true,
                    Active = true
                });
                return Ok();
            }           
            return Ok();
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
