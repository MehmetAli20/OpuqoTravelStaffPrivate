using BusinessLayer.Abstract;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.TravelDTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json;
using NToastNotify;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using TravelStaff.Models;

namespace TravelStaff.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IToastNotification toastNotification;
        private readonly string _baseUrl = "https://localhost:7143";
        

        public AuthController(ILogger<AuthController> logger, IHttpClientFactory httpClientFactory, IToastNotification toastNotification)
        {
            _logger = logger;
            this.httpClientFactory = httpClientFactory;
            this.toastNotification = toastNotification;
        }
        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                toastNotification.AddErrorToastMessage("Alanları Dolurunuz ve Tekrar Deneyiniz.");
                return RedirectToAction("Login", "Auth", new {authLayoutDto=new AuthLayoutDto{ RegisterDto = registerDto }});
            }
            
            HttpClient client = httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(registerDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(_baseUrl + "/api/auth/register", data);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<RegisterReturnDto>(responseData);

                if (result.Success)
                {                  
                    
                    return RedirectToAction("Login", "Auth");
                }
                else
                {
                    Console.WriteLine("ERRRRRRROOOOOOOOOOORRRRRRRRRR");
                    ModelState.AddModelError(string.Empty, result.ErrorMessage);
                }
            }
            else
            {
                _logger.LogError("API request failed with status code: {StatusCode}", response.StatusCode);
                ModelState.AddModelError(string.Empty, "An error occurred while registering. Please try again.");
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpGet]
        public async Task<IActionResult> Login(AuthLayoutDto authLayoutDto)
        {
            return View(authLayoutDto);
        }
        
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {           
            HttpClient client = httpClientFactory.CreateClient();
            var json = JsonConvert.SerializeObject(loginDto);
            var data = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync(_baseUrl + "/api/auth/login", data);
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<LoginReturnDto>(responseData);
                HttpContext.Session.SetString("userId", result.Id.ToString());
                HttpContext.Session.SetString("adminId", result.AdminID.ToString());
                
                if (result.Success && result.IsAdmin)
                {
                    HttpContext.Session.SetString("isAdmin", result.IsAdmin.ToString());                   
                    return RedirectToAction("Admin", "Auth", new { userId = result.Id }); 
                }
                else if (result.Success == true && result.IsAdmin == false)
                {
                    return RedirectToAction("Staff", "Auth", new { userId = HttpContext.Session.GetString("userId") });
                }
                else if (result.Success == false)
                {
                    return RedirectToAction("Login", "Auth");
                }

            }
            return Redirect(Request.Headers["Referer"].ToString());
        }

        [HttpGet]
        public async Task<IActionResult> Admin(int userId)
        {
            bool isAdmin = HttpContext.Session.GetString("isAdmin") == "True";
            ViewBag.isAdmin = isAdmin;
            HttpClient client = httpClientFactory.CreateClient();
            ViewBag.userId = userId;           
            HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/staff/getstaffbyadminid/{userId}");
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var adminList = JsonConvert.DeserializeObject<List<GetStaffDto>>(responseData);

                return View(adminList);
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Staff(int userId)
        {
            bool isAdmin = HttpContext.Session.GetString("isAdmin") == "True";
            ViewBag.isAdmin = isAdmin;
            ViewBag.userId = userId;
            HttpClient client = httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync($"{_baseUrl}/api/travel/getstaffstravels/{userId}"); // -----------buradaki API ile Travel çekilecek ve Viewa verilecek -----------
            
            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                var staffList = JsonConvert.DeserializeObject<List<GetTravelDto>>(responseData);
               
                return View(staffList);
            }

            _logger.LogError("API request failed with status code: {StatusCode}", response.StatusCode);
            return View(new List<GetTravelDto>()); // API çağrısı başarısız olursa boş liste döndür
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}
