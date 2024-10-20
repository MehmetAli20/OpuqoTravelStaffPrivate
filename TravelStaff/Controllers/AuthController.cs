using BusinessLayer.Abstract;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.TravelDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration.UserSecrets;
using Newtonsoft.Json;
using NToastNotify;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
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
            
            HttpClient client = httpClientFactory.CreateClient("TravelStaff");
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
            try
            {
                HttpClient client = httpClientFactory.CreateClient("TravelStaff");
                if (client == null)
                {
                    _logger.LogError("HttpClient 'TravelStaff' is null.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "HttpClient configuration issue.");
                }

                if (string.IsNullOrEmpty(_baseUrl))
                {
                    _logger.LogError("Base URL is null or empty.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Base URL configuration issue.");
                }

                var json = JsonConvert.SerializeObject(loginDto);
                var data = new StringContent(json, Encoding.UTF8, "application/json");
                if (data == null)
                {
                    _logger.LogError("Data content is null.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "Request data issue.");
                }

                HttpResponseMessage response = await client.PostAsync(_baseUrl + "/api/auth/login", data);
                if (response == null)
                {
                    _logger.LogError("Response is null.");
                    return StatusCode(StatusCodes.Status500InternalServerError, "API response issue.");
                }

                if (response.IsSuccessStatusCode)
                {
                    var responseData = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<LoginReturnDto>(responseData);

                    if (result == null)
                    {
                        _logger.LogWarning("Deserialized result is null. Response data: {ResponseData}", responseData);
                        return StatusCode(StatusCodes.Status500InternalServerError, "Error processing login response.");
                    }

                    HttpContext.Session.SetString("AuthToken", result.Token);
                    HttpContext.Session.SetString("userId", result.Id.ToString());
                    HttpContext.Session.SetString("adminId", result.AdminID.ToString());
                    HttpContext.Session.SetString("isAdmin", result.IsAdmin.ToString().ToLower());

                    if (result.Success && result.IsAdmin)
                    {
                        return RedirectToAction("Admin", "Auth", new { userId = result.Id });
                    }
                    else if (result.Success && !result.IsAdmin)
                    {
                        return RedirectToAction("Staff", "Auth", new { userId = HttpContext.Session.GetString("userId") });
                    }
                    else
                    {
                        return RedirectToAction("Login", "Auth");
                    }
                }
                else
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    _logger.LogError("Login failed. Status code: {StatusCode}. Response content: {ResponseContent}", response.StatusCode, errorContent);
                    return StatusCode(StatusCodes.Status500InternalServerError, "Login failed. Please try again.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the login process.");
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred. Please try again.");
            }
        }

        [HttpGet]
        public async Task<IActionResult> HomePage()
        {
            bool isAdmin = HttpContext.Session.GetString("isAdmin") == "true";
            ViewBag.isAdmin = isAdmin;
            HttpClient client = httpClientFactory.CreateClient("TravelStaff");
            
            var token = HttpContext.Session.GetString("AuthToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (isAdmin)
            {
                return RedirectToAction("Admin", "Auth", new { userId = HttpContext.Session.GetString("userId") });
            }
            else
            {
                return RedirectToAction("Staff", "Auth", new { userId = HttpContext.Session.GetString("userId") });
            }          
        }

        [HttpGet]
        public async Task<IActionResult> Admin(int userId)
        {           
            bool isAdmin = HttpContext.Session.GetString("isAdmin") == "true";
            ViewBag.isAdmin = isAdmin;
            ViewBag.userId = userId;        
            HttpClient client = httpClientFactory.CreateClient("TravelStaff");
            var token = HttpContext.Session.GetString("AuthToken");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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
            HttpClient client = httpClientFactory.CreateClient("TravelStaff");
            var token = HttpContext.Session.GetString("AuthToken");
            bool isAdmin = HttpContext.Session.GetString("isAdmin") == "True";
            
            ViewBag.isAdmin = isAdmin;
            ViewBag.userId = userId;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            // Clear session data (tokens, user information)
            HttpContext.Session.Clear();

            // Optionally show a toast notification for successful logout
            toastNotification.AddSuccessToastMessage("Çıkış yapıldı.");

            // Redirect to login page after logout
            return RedirectToAction("Login", "Auth");
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }

}
