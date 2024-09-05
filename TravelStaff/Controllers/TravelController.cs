using EntityLayer.Concrete;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.TravelDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace TravelStaff.Controllers
{
	public class TravelController : Controller
	{
        private readonly ILogger<TravelController> _logger;
        private readonly IHttpClientFactory httpClientFactory;
        //private readonly UserManager<Staff> _userManager;
        private readonly string _baseUrl = "https://localhost:7143";
        public TravelController(ILogger<TravelController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        [HttpGet]
        public async Task<IActionResult> Travels(int id)
        {
            bool isAdmin = HttpContext.Session.GetString("isAdmin") == "True";
            ViewBag.isAdmin = isAdmin;
            var userId = HttpContext.Session.GetString("userId");
            HttpClient client = httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/travel/getstaffstravels/{id}");           
            
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Index", "Home");
            }

            if (response.IsSuccessStatusCode)
            {      
                ViewBag.userId = userId;
                string responseData = await response.Content.ReadAsStringAsync();
                var allTravels = JsonConvert.DeserializeObject<List<GetTravelDto>>(responseData);
                //if (allTravels == null)
                //{
                //    return View(new List<GetTravelDto>());
                //}
                var filteredTravels = allTravels;

                return View(filteredTravels);
            }

            return View(new List<GetTravelDto>());
        }

        [HttpPost]
        public IActionResult BackToPreviousPage()
        {
            bool isAdmin = HttpContext.Session.GetString("isAdmin") == "True";
            ViewBag.isAdmin = isAdmin;
            if (TempData["PreviousUrl"] != null)
            {
                string previousUrl = TempData["PreviousUrl"].ToString();
                return Redirect(previousUrl);
            }

            return RedirectToAction("Index", "Home"); // Eğer URL bulunamazsa anasayfaya yönlendirin
        }
    
        [HttpGet]
        public async Task<IActionResult> TravelDetails(int travelId)
        {
            bool isAdmin = HttpContext.Session.GetString("isAdmin") == "True";
            ViewBag.isAdmin = isAdmin;
            TempData["PreviousUrl"] = Request.Headers["Referer"].ToString();
            HttpClient client = httpClientFactory.CreateClient();
            var userId = HttpContext.Session.GetString("userId");          
            ViewBag.userId = userId;
            HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/travel/getbyid/{travelId}");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var travelDetail = JsonConvert.DeserializeObject<GetTravelDto>(responseData);

                if (travelDetail != null)
                {
                    return View(travelDetail);
                }
            }

            return View(new GetTravelDto()); 
        }

        [HttpGet]
        public async Task<IActionResult> UsersTravels()
        {
            try
            {
                bool isAdmin = HttpContext.Session.GetString("isAdmin") == "True";
                ViewBag.isAdmin = isAdmin;
                var userId = HttpContext.Session.GetString("userId");
                HttpClient client = httpClientFactory.CreateClient();
                HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/travel/getstaffstravels/{userId}");

                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Index", "Home");
                }

                if (response.IsSuccessStatusCode)
                {
                    ViewBag.userId = userId;
                    string responseData = await response.Content.ReadAsStringAsync();
                    var allTravels = JsonConvert.DeserializeObject<List<GetTravelDto>>(responseData);
                    //if (allTravels == null)
                    //{
                    //    return View(new List<GetTravelDto>());
                    //}
                    var filteredTravels = allTravels; //filtreleme işlemi burada yapılabilir  <------------

                    return View(filteredTravels);
                }

                return View(new List<GetTravelDto>());
            }
            catch (Exception ex)
            {
                var test = ex;
                return RedirectToAction("Index", "Home");
            }
            
            //bool isAdmin = HttpContext.Session.GetString("isAdmin") == "True";
            //ViewBag.isAdmin = isAdmin;
            //var userId = HttpContext.Session.GetString("userId");
            //HttpClient client = httpClientFactory.CreateClient();
            //HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/travel/getstaffstravels/{userId}");

            //if (string.IsNullOrEmpty(userId))
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            //if (response.IsSuccessStatusCode)
            //{
            //    ViewBag.userId = userId;
            //    string responseData = await response.Content.ReadAsStringAsync();
            //    var allTravels = JsonConvert.DeserializeObject<List<GetTravelDto>>(responseData);
            //    //if (allTravels == null)
            //    //{
            //    //    return View(new List<GetTravelDto>());
            //    //}
            //    var filteredTravels = allTravels;

            //    return View(filteredTravels);
            //}

            //return View(new List<GetTravelDto>());
        }

        [HttpGet]
        public async Task<IActionResult> AddTravel()
        {
            bool isAdmin = HttpContext.Session.GetString("isAdmin") == "True";
            ViewBag.isAdmin = isAdmin;
            var userId = HttpContext.Session.GetString("userId");
            var adminId = HttpContext.Session.GetString("adminId");
            ViewBag.userId = userId;
            ViewBag.adminId = adminId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTravel(CreateTravelDto createTravelDto)
        {
            bool isAdmin = HttpContext.Session.GetString("isAdmin") == "True";
            ViewBag.isAdmin = isAdmin;
            var userId = HttpContext.Session.GetString("userId");
            var adminId = HttpContext.Session.GetString("adminId");
            ViewBag.userId = userId;
          
            if (userId != null && adminId != null)
            {
                createTravelDto.Id = int.Parse(userId); 
                createTravelDto.AdminID = int.Parse(adminId); 

                HttpClient client = httpClientFactory.CreateClient();
                var json = JsonConvert.SerializeObject(createTravelDto);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(_baseUrl + "/api/travel/create", data);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Travels", new { id = createTravelDto.Id });
                }
            }
            return View(createTravelDto);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateTravel(int id)
        {
            HttpContext.Session.SetString("travelId", id.ToString());
            bool isAdmin = HttpContext.Session.GetString("isAdmin") == "True";
            ViewBag.isAdmin = isAdmin;
            HttpClient client = httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/travel/getbyid/{id}");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var travel = JsonConvert.DeserializeObject<GetTravelDto>(responseData);
                return View(travel);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTravel(UpdateTravelDto updateTravelDto)
        {
            var travelId = HttpContext.Session.GetString("travelId");
            bool isAdmin = HttpContext.Session.GetString("isAdmin") == "True";
            ViewBag.isAdmin = isAdmin;
            HttpClient client = httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(updateTravelDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync(_baseUrl + "/api/travel/update/" + travelId, data);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Travels", new {id = int.Parse(HttpContext.Session.GetString("userId"))}) ;
            }
            return View();
        }

        //[HttpDelete]
        //public async Task<IActionResult> DeleteTravel(int id)
        //{
        //    HttpClient client = httpClientFactory.CreateClient();
        //    HttpResponseMessage response = await client.DeleteAsync($"{_baseUrl}/api/travel/delete/{id}");
        //    if (response.IsSuccessStatusCode)
        //    {
        //        return RedirectToAction("Travels");
        //    }
        //    return View();
        //}               
    }
}
