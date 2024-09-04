using Microsoft.AspNetCore.Identity;
using EntityLayer.DTOs;
using BusinessLayer.Abstract;
using EntityLayer.DTOs.StaffDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.Concrete;
using System.Xml.Linq;
using System;

namespace TravelStaffAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IStaffService _staffService;
        private readonly UserManager<Staff> _userManager;
        private readonly SignInManager<Staff> _signInManager;

        public AuthController(IStaffService staffService, UserManager<Staff> userManager, SignInManager<Staff> signInManager)
        {
            _staffService = staffService;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<LoginReturnDto> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return(new LoginReturnDto { Success = false, Message = "Invalid login credentials." });
            }

            // Find the user by their username
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                return(new LoginReturnDto { Success = false, Message = "Invalid login credentials." });
            }

            // Validate the password
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                // Sign the user in
                await _signInManager.SignInAsync(user, isPersistent: false);

                var redirectUrl = user.IsAdmin ? "/Admin" : "/Staff";
                // Return a success response
                return new LoginReturnDto
                {
                    Success = true,
                    RedirectUrl = redirectUrl,
                    Id = user.Id,
                    IsAdmin = user.IsAdmin,
                    AdminID = user.AdminID.Value,
                }; 
            }

            // If the login fails, return an unauthorized response
            return (new LoginReturnDto { Success = false, Message = "Invalid login credentials." });
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            // Model doğrulamasını kontrol et
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Yeni bir Staff nesnesi oluştur


            try
            {
                // Kullanıcıyı oluştur
                var result = await _userManager.CreateAsync(new()
                {
                    UserName = registerDto.UserName,
                    Name = registerDto.Name,
                    Surname = registerDto.Surname,
                    Email = registerDto.Email,
                    Active = true,
                    IsAdmin = registerDto.IsAdmin,
                    AdminID = 44,
                    Password="test"
                }, registerDto.Pw);

                // result nesnesinin null olup olmadığını kontrol et
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "Unexpected error occurred while creating user.");
                }

                // Eğer başarılı ise
                if (result.Succeeded)
                {
                    return Ok(new RegisterReturnDto { Success = true });
                }
            }
            catch (Exception ex)
            {

                var test = ex.Message;
            }

            
                //// Hataları işle
                //var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest(new RegisterReturnDto { Success = false, ErrorMessage = "" });
          

        }

    }
}
