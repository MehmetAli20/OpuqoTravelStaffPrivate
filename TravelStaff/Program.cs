using Autofac;
using Autofac.Extensions.DependencyInjection;
using BusinessLayer.DependencyResolvers.Autofac;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NToastNotify;
using System.Net.Http.Headers;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configure Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacBusinessModule());
});

// Configure services
builder.Services.AddDbContext<Context>();
//builder.Services.AddIdentity<Staff, Role<int>>()
builder.Services.AddIdentity<Staff, Role<int>>()
    .AddEntityFrameworkStores<Context>()
    .AddDefaultTokenProviders();

// Read JWT settings from configuration
var jwtKey = builder.Configuration["Jwt:Key"];
if (string.IsNullOrEmpty(jwtKey))
{
    throw new InvalidOperationException("JWT Key is not configured.");
}

var key = Encoding.ASCII.GetBytes(jwtKey);

// Configure JWT authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
{
    options.LoginPath = "/Auth/Login";
    options.LogoutPath = "/Auth/Logout";
});

// Add HttpClient with base address and timeout
builder.Services.AddHttpClient("TravelStaff", client =>
{
    client.BaseAddress = new Uri("https://localhost:7143");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.Timeout = TimeSpan.FromMinutes(30);
});

// Configure NToastNotify for toastr notifications
builder.Services.AddControllersWithViews().AddNToastNotifyToastr(new NToastNotify.ToastrOptions
{
    ProgressBar = true,
    PositionClass = ToastPositions.TopCenter,
    PreventDuplicates = true,
    CloseButton = true,
    ShowDuration = 5000,
    HideDuration = 5000,
    TimeOut = 5000,
    ExtendedTimeOut = 5000,
    ShowEasing = "swing",
    HideEasing = "linear",
    ShowMethod = "fadeIn",
    HideMethod = "fadeOut"
});

// Configure session storage
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Session timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Add authentication middleware
app.UseAuthorization();  // Add authorization middleware

app.UseSession(); // Enable session middleware

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
