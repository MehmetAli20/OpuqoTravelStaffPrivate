using EntityLayer.DTOs.MessageDTOs;
using EntityLayer.DTOs.TravelDTOs;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using TravelStaff.Models;

namespace TravelStaff.Controllers
{
    public class MessageController : Controller
    {
        private readonly ILogger<MessageController> _logger;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly string _baseUrl = "https://localhost:7143";

        public MessageController(ILogger<MessageController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this.httpClientFactory = httpClientFactory;

        }

        [HttpGet]
        public async Task<IActionResult> GetMessageByTravel(int travelId)
        {
            bool isFromAdmin = HttpContext.Session.GetString("isFromAdmin") == "true";
            var travelMessageLayoutDto = new TravelMessageLayoutDto();
            
            // Mesajları getirme işlemi
            HttpClient client = httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/message/getbytravelId/{travelId}/{isFromAdmin}");

            if (response.IsSuccessStatusCode)
            {
                var responseData = await response.Content.ReadAsStringAsync();
                var messages = JsonConvert.DeserializeObject<List<GetMessageDto>>(responseData);
                
                ViewBag.isFromAdmin = (travelMessageLayoutDto.getTravelDto.AdminID == int.Parse(HttpContext.Session.GetString("userId")) ? true : false);               
                
                travelMessageLayoutDto.getMessageDto = messages;
                travelMessageLayoutDto.getTravelDto = new GetTravelDto { TravelID = travelId }; // TravelID'yi formda kullanmak için ekliyoruz
            }
            else
            {
                _logger.LogError("API request failed with status code: {StatusCode}", response.StatusCode);
            }

            // Boş CreateMessageDto ile sayfayı döndür
            travelMessageLayoutDto.createMessageDto = new CreateMessageDto();
            return View(travelMessageLayoutDto);
        }

        [HttpPost]
        public async Task<IActionResult> GetMessageByTravel(TravelMessageLayoutDto travelMessageLayoutDto)
        {
            bool isAdmin = HttpContext.Session.GetString("isAdmin") == "true";
            int userId = Convert.ToInt32(HttpContext.Session.GetString("userId"));

            HttpClient client = httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(travelMessageLayoutDto.createMessageDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            travelMessageLayoutDto.createMessageDto.FromAdmin = isAdmin; //HATA VAR BURADA BURAYA AT DEBUG
            travelMessageLayoutDto.createMessageDto.UserID = userId;

            HttpResponseMessage response = await client.PostAsync($"{_baseUrl}/api/message/create", data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("GetMessageByTravel", new { travelId = travelMessageLayoutDto.getTravelDto.TravelID });
            }

            _logger.LogError("API request failed with status code: {StatusCode}", response.StatusCode);
            return View(travelMessageLayoutDto);
        }
        
        //[HttpPost]
        //public IActionResult CreateMessage([FromBody] CreateMessageDto createMessageDto)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var message = new Message
        //        {
        //            TravelID = createMessageDto.TravelID,
        //            Content = createMessageDto.Content,
        //            SendDate = DateTime.Now,
        //            FromAdmin = false, // Burada admin olup olmadığını ayarlayabilirsiniz
        //            Active = true
        //        };

        //        _IMessageService.TAdd(message);
        //        return Ok();
        //    }

        //    return BadRequest();
        //}

    }

    //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    //    public IActionResult Error()
    //    {
    //        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    //    }
    //}
}
