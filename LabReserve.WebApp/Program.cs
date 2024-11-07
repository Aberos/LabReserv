using LabReserve.WebApp.Domain.Enums;
using LabReserve.WebApp.Infrastructure;
using LabReserve.WebApp.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureInfrastructureApp(builder.Configuration);
builder.Services.ConfigureServicesApp();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "auth";
        options.ExpireTimeSpan = TimeSpan.FromHours(24);
        options.LoginPath = "/user/auth";
        options.AccessDeniedPath = "/user/access-denied";
    });

builder.Services.AddAuthorizationBuilder()
    .AddPolicy("professor", policy => policy.RequireClaim(ClaimTypes.Role, UserType.Professor.ToString()))
    .AddPolicy("manager", policy => policy.RequireClaim(ClaimTypes.Role, UserType.Manager.ToString()))
    .AddPolicy("admin", policy => policy.RequireClaim(ClaimTypes.Role, UserType.Admin.ToString()));

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