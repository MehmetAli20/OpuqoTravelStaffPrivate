using Microsoft.AspNetCore.Identity;
using EntityLayer.DTOs;
using BusinessLayer.Abstract;
using EntityLayer.DTOs.StaffDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EntityLayer.Concrete;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

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
        private readonly ILogger<AuthController> _logger;
        private readonly IConfiguration _configuration;

        public AuthController(IStaffService staffService, UserManager<Staff> userManager, SignInManager<Staff> signInManager, ILogger<AuthController> logger, IConfiguration configuration)
        {
            _staffService = staffService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (loginDto == null)
            {
                _logger.LogError("LoginDto is null.");
                return BadRequest(new { Message = "Invalid login request." });
            }

            try
            {
                var user = await _userManager.FindByNameAsync(loginDto.UserName);
                if (user == null)
                {
                    _logger.LogWarning("User not found: {UserName}", loginDto.UserName);
                    return Unauthorized(new { Message = "Invalid username or password." });
                }

                var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, lockoutOnFailure: false);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Invalid password for user: {UserName}", loginDto.UserName);
                    return Unauthorized(new { Message = "Invalid username or password." });
                }

                // Generate JWT token
                var token = GenerateJwtToken(user);

                return Ok(new LoginReturnDto
                {
                    //RedirectUrl = .... 
                    Message = "Token Generated Succesfully",
                    Success = true,
                    Token = token,
                    Id = user.Id,
                    AdminID = user.AdminID ?? 44,
                    IsAdmin = user.IsAdmin
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during the login process.");
                return StatusCode(StatusCodes.Status500InternalServerError, new { Message = "An unexpected error occurred. Please try again." });
            }
        }

        private string GenerateJwtToken(Staff user)
        {

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.UserName != null)
            {
                _logger.LogInformation("User: {UserName} is logging in.", user.UserName);
                _logger.LogInformation("User: {Name} is logging in.", user.Name);
            }

            if (user.Id == null || user.Id == 0)
            {
                throw new ArgumentNullException(nameof(user.Id));
            }

            if (user.IsAdmin == null)
            {
                throw new ArgumentNullException(nameof(user.IsAdmin));
            }

            if (user.AdminID == null)
            {
                throw new ArgumentNullException(nameof(user.AdminID));
            }
            // Check if _configuration is null
            if (_configuration == null)
            {
                throw new InvalidOperationException("Configuration is not set.");
            }
            var claims = new[]
            {
        new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? ""), // Null kontrolü
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim("userId", user.Id.ToString() ?? "0"), // Null kontrolü
        new Claim("adminId", user.AdminID?.ToString() ?? "0"), // Null kontrolü
        new Claim("isAdmin", user.IsAdmin.ToString().ToLower()) // Genelde bool değer, null olmayabilir
    };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            // Model doğrulamasını kontrol et
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                // Kullanıcıyı oluştur
                var result = await _userManager.CreateAsync(new Staff
                {
                    UserName = registerDto.UserName,
                    Name = registerDto.Name,
                    Surname = registerDto.Surname,
                    Email = registerDto.Email,
                    Active = true,
                    IsAdmin = registerDto.IsAdmin,
                    AdminID = 44,
                }, registerDto.Password);
                if (!ModelState.IsValid)
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    // Hataları inceleyin
                }
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
                // Hataları işle
                return StatusCode(StatusCodes.Status500InternalServerError, new RegisterReturnDto { Success = false, ErrorMessage = ex.Message });
            }

            return BadRequest(new RegisterReturnDto { Success = false, ErrorMessage = "Failed to register user." });
        }
    }
}
