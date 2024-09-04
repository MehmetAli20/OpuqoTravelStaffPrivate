using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using BusinessLayer.DependencyResolvers.Autofac;
using DataAccessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// DbContext ve Identity konfig�rasyonlar�
builder.Services.AddDbContext<Context>();
builder.Services.AddIdentity<Staff, Role<int>>()
    .AddEntityFrameworkStores<Context>();

// MVC ve Razor ayarlar�
builder.Services.AddControllersWithViews();

builder.Services.AddMvc(config =>
{
    var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    config.Filters.Add(new AuthorizeFilter(policy));
});
builder.Services.AddMvc();

// CORS ayarlar�
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("https://localhost:7055") // �zin verilen kaynak
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

// JSON se�enekleri
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// AutoMapper ayarlar�
builder.Services.AddAutoMapper(typeof(Program));

// Swagger/OpenAPI ayarlar�
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Session konfig�rasyonu
builder.Services.AddDistributedMemoryCache(); // Session veri saklama i�in gerekli
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// HttpContextAccessor ayarlar�
builder.Services.AddHttpContextAccessor(); // HttpContext eri�imi i�in gerekli

// Autofac ayarlar�
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacBusinessModule());
});

var app = builder.Build();

// Middleware ve pipeline konfig�rasyonu
app.UseCors("AllowSpecificOrigins");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseSession(); // Session middleware
app.MapControllers();

app.Run();
