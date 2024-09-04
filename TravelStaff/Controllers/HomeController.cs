using EntityLayer.Concrete;
using EntityLayer.DTOs.StaffDTOs;
using EntityLayer.DTOs.TravelDTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using TravelStaff.Models;
using static System.Net.WebRequestMethods;

namespace TravelStaff.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory httpClientFactory;
        private readonly string _baseUrl = "https://localhost:7143";

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            this.httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Travels(int i)
        {
            //HttpClient client = httpClientFactory.CreateClient();
            //HttpResponseMessage response = await client.GetAsync(_baseUrl+"/api/travel/getall");
            //var data = await response.Content.ReadFromJsonAsync<List<GetTravelDto>>();
            //Console.WriteLine(data);
            //if(response.IsSuccessStatusCode)
            //{
            //    return View(data);
            //}
            //return Redirect(Request.Headers["Referer"].ToString());
            return View();
        }
        public async Task<IActionResult> TravelDetails(int staffId)
        {
            //HttpClient client = httpClientFactory.CreateClient();
            //HttpResponseMessage response = await client.GetAsync(_baseUrl + "/api/travel/getbyid?id=" + id);
            //var data = await response.Content.ReadFromJsonAsync<GetTravelDto>();
            //if (response.IsSuccessStatusCode)
            //{
            //    return View(data);
            //}
            //return Redirect(Request.Headers["Referer"].ToString());
            return View();
        }
        public async Task<IActionResult> Staff()
        {           
            return View();
        }

        public async Task<IActionResult> Admin()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }
        
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        
        //public IActionResult Index()
                //{
                //    ViewBag.TURTLE = "ViewBag";
                //    List<Staff> List = new List<Staff>() { new Staff { StaffID=1,Name="Attempt", Surname="Method" }, new Staff { StaffID = 2, Name = "Dummy", Surname = "Data" }, new Staff { StaffID = 3, Name = "Dummy1", Surname = "Data1" } };
                //    return View(List);
                //}
        
      
    }
}