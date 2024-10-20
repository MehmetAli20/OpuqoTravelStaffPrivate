using EntityLayer.Concrete;
using EntityLayer.DTOs.MessageDTOs;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.TravelDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using NuGet.Common;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace TravelStaff.Controllers
{
    public class TravelController : Controller
    {
        private readonly ILogger<TravelController> _logger;
        //private readonly HttpClient _client;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly UserManager<Staff> _userManager;
        private readonly string _baseUrl = "https://localhost:7143";

        public TravelController(ILogger<TravelController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this.httpClientFactory = httpClientFactory;

        }

        [HttpGet]
        public async Task<IActionResult> Travels(int id)
        {
            HttpClient client = httpClientFactory.CreateClient("TravelStaff");
            var isAdmin = bool.Parse(HttpContext.Session.GetString("isAdmin") ?? "false");
            ViewBag.isAdmin = isAdmin;
            var userId = HttpContext.Session.GetString("userId");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            // Token'ı header'a ekleyin
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync($"/api/travel/getstaffstravels/{id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"Error: {response.StatusCode} - {errorContent}");
                // Daha fazla hata teşhis bilgisi eklemek için:
                _logger.LogError($"Response Headers: {response.Headers.ToString()}");
                _logger.LogError($"Response Reason Phrase: {response.ReasonPhrase}");
            }
            if (response.IsSuccessStatusCode)
            {
                ViewBag.userId = userId;
                string responseData = await response.Content.ReadAsStringAsync();
                var allTravels = JsonConvert.DeserializeObject<List<GetTravelDto>>(responseData) ?? new List<GetTravelDto>();
                return View(allTravels);
            }

            return View(new List<GetTravelDto>());
        }
        [HttpPost]
        public IActionResult BackToPreviousPage()
        {
            var previousUrl = TempData["PreviousUrl"]?.ToString() ?? Url.Action("Index", "Home");
            return Redirect(previousUrl);
        }

        [HttpGet]
        public async Task<IActionResult> TravelDetails(int travelId)
        {
            HttpClient client = httpClientFactory.CreateClient("TravelStaff");
            var userId = HttpContext.Session.GetString("userId");
            var isAdmin = bool.Parse(HttpContext.Session.GetString("isAdmin") ?? "false");
            ViewBag.isAdmin = isAdmin;

            TempData["PreviousUrl"] = Request.Headers["Referer"].ToString();
            ViewBag.userId = userId;
            ViewBag.travelId = travelId;

            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }
            
            // Token'ı header'a ekleyin
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/travel/getbyid/{travelId}");

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var travelDetail = JsonConvert.DeserializeObject<TravelMessageLayoutDto>(responseData);

                if (travelDetail?.getTravelDto != null)
                {
                    //ViewBag.isFromAdmin = int.Parse(userId) == travelDetail.getTravelDto.AdminID;
                    return View(travelDetail);
                }
            }

            return View(new TravelMessageLayoutDto());
        }

        [HttpGet]
        public async Task<IActionResult> UsersTravels()
        {
            try
            {
                HttpClient client = httpClientFactory.CreateClient("TravelStaff");
                var isAdmin = bool.Parse(HttpContext.Session.GetString("isAdmin") ?? "false");
                ViewBag.isAdmin = isAdmin;
                var userId = HttpContext.Session.GetString("userId");
                ViewBag.userId = userId;


                var token = HttpContext.Session.GetString("AuthToken");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Index", "Home");
                }

                HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/travel/getstaffstravels/{userId}");

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    var allTravels = JsonConvert.DeserializeObject<List<GetTravelDto>>(responseData);
                    return View(allTravels);
                }

                return View(new List<GetTravelDto>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving user's travels.");
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult AddTravel()
        {
            var isAdmin = HttpContext.Session.GetString("isAdmin")?.ToLower() == "true";
            ViewBag.isAdmin = isAdmin;
            ViewBag.userId = HttpContext.Session.GetString("userId");
            ViewBag.adminId = HttpContext.Session.GetString("adminId");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTravel(CreateTravelDto createTravelDto)
        {
            HttpClient client = httpClientFactory.CreateClient("TravelStaff");
            var userId = HttpContext.Session.GetString("userId");
            var adminId = HttpContext.Session.GetString("adminId");

            if (userId != null && adminId != null)
            {
                createTravelDto.Id = int.Parse(userId);
                createTravelDto.AdminID = int.Parse(adminId);

                var json = JsonConvert.SerializeObject(createTravelDto);
                var data = new StringContent(json, Encoding.UTF8, "application/json");

                var token = HttpContext.Session.GetString("AuthToken");
                if (string.IsNullOrEmpty(token))
                {
                    return RedirectToAction("Login", "Auth");
                }

                // Token'ı header'a ekleyin
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await client.PostAsync($"{_baseUrl}/api/travel/create", data);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Travels", new { id = createTravelDto.Id });
                }
            }

            ViewBag.isAdmin = bool.Parse(HttpContext.Session.GetString("isAdmin") ?? "false");
            ViewBag.userId = userId;
            return View(createTravelDto);
        }

        [HttpGet]
        public async Task<IActionResult> EditTravel(int travelId)
        {
            HttpClient client = httpClientFactory.CreateClient("TravelStaff");
            var userId = int.Parse(HttpContext.Session.GetString("userId"));
            var isAdmin = bool.Parse(HttpContext.Session.GetString("isAdmin") ?? "false");
            ViewBag.isAdmin = isAdmin;
            ViewBag.userId = userId;
            ViewBag.travelId = travelId;
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/travel/getbyidupdate/{travelId}");

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var travelDetail = JsonConvert.DeserializeObject<UpdateTravelDto>(responseData);

                if (travelDetail != null)
                {
                    return View(travelDetail);
                }
            }

            return View(new UpdateTravelDto());
        }


        [HttpPost]
        public async Task<IActionResult> EditTravel(UpdateTravelDto updateTravelDto)
        {
            HttpClient client = httpClientFactory.CreateClient("TravelStaff");

            // Kullanıcı ID ve admin durumunu al
            var userId = HttpContext.Session.GetString("userId");
            var isAdmin = bool.Parse(HttpContext.Session.GetString("isAdmin") ?? "false");
            ViewBag.isAdmin = isAdmin;

            // Güncellenecek seyahatin ID'sini al
            var travelId = updateTravelDto.TravelID;

            if (userId == null)
            {
                return RedirectToAction("Login", "Auth"); 
            }

            var json = JsonConvert.SerializeObject(updateTravelDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            // Token'ı al ve kontrol et
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth"); 
            }

            // Token'ı header'a ekle
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.PutAsync($"{_baseUrl}/api/travel/update/{travelId}", data);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Travels", new { id = userId }); //burada userid yerine secili travelin kullanici idsin alinsin veya baska sayfsya yonlendirilsin
            }

            return View(updateTravelDto);
        }



        [HttpPost]
        public async Task<IActionResult> DeleteTravel(int travelId)
        {
            HttpClient client = httpClientFactory.CreateClient("TravelStaff");
            var token = HttpContext.Session.GetString("AuthToken");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Login", "Auth");
            }

            // Token'ı header'a ekleyin
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.DeleteAsync($"{_baseUrl}/api/travel/delete/{travelId}");

            if (response.IsSuccessStatusCode)
            {               
                return RedirectToAction("Travels", new { id = int.Parse(HttpContext.Session.GetString("userId")) });
            }

            return RedirectToAction("Travels", new { id = int.Parse(HttpContext.Session.GetString("userId")) });
        }

        [HttpGet]
        public async Task <IActionResult> GetPastTravelsByUserId()
        {
            HttpClient client = httpClientFactory.CreateClient("TravelStaff");
            var userId = HttpContext.Session.GetString("userId");
            ViewBag.userId = userId;
            var token = HttpContext.Session.GetString("AuthToken");

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/travel/GetPastTravelsByUserId/{userId}");

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var allTravels = JsonConvert.DeserializeObject<List<GetTravelDto>>(responseData);
                return View(allTravels);
            }

            return View(new List<GetTravelDto>());
        }
    }
}



 //[HttpGet]
        //public async Task<IActionResult> UpdateTravel(int id)
        //{
        //    HttpContext.Session.SetString("travelId", id.ToString());
        //    bool isAdmin = HttpContext.Session.GetString("isAdmin") == "True";
        //    ViewBag.isAdmin = isAdmin;
        //    HttpClient client = httpClientFactory.CreateClient();
        //    HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/travel/getbyid/{id}");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        string responseData = await response.Content.ReadAsStringAsync();
        //        var travel = JsonConvert.DeserializeObject<GetTravelDto>(responseData);
        //        return View(travel);
        //    }
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> UpdateTravel(UpdateTravelDto updateTravelDto)
        //{
        //    var travelId = HttpContext.Session.GetString("travelId");
        //    bool isAdmin = HttpContext.Session.GetString("isAdmin") == "True";
        //    ViewBag.isAdmin = isAdmin;
        //    HttpClient client = httpClientFactory.CreateClient();
        //    var json = JsonConvert.SerializeObject(updateTravelDto);
        //    var data = new StringContent(json, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = await client.PutAsync(_baseUrl + "/api/travel/update/" + travelId, data);
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Travels", new { id = int.Parse(HttpContext.Session.GetString("userId")) });
        //    }
        //    return View();
        //}