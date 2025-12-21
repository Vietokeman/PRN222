using Microsoft.AspNetCore.Authentication.Cookies;
using RealEstateManagement.Services;
using RealEstateManagement_NguyenViet;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.AccessDeniedPath = "/Forbidden";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
        options.LoginPath = "/Login";
    });

builder.Services.AddScoped<SystemUserService>();
builder.Services.AddScoped<ContractService>();
builder.Services.AddScoped<BrokerService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapHub<NotificationHub>("/notificationHub");
app.MapRazorPages().RequireAuthorization();


app.Run();
