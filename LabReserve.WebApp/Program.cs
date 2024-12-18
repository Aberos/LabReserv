using LabReserve.Application;
using LabReserve.Domain.Enums;
using LabReserve.Persistence;
using LabReserve.WebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureInfrastructureApp(builder.Configuration);
builder.Services.ConfigureApplicationApp();
builder.Services.ConfigureServicesWebApp();

// Add services to the container.
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNameCaseInsensitive = false;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth";
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
        options.LoginPath = "/auth";
        options.AccessDeniedPath = "/auth/access-denied";
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("professor", policy => policy.RequireClaim(ClaimTypes.Role, UserType.Professor.ToString()))
    .AddPolicy("manager", policy => policy.RequireClaim(ClaimTypes.Role, UserType.Manager.ToString()))
    .AddPolicy("admin", policy => policy.RequireClaim(ClaimTypes.Role, UserType.Admin.ToString()));


builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();