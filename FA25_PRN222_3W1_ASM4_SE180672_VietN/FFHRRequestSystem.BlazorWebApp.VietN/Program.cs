using FFHRRequestSystem.BlazorWebApp.VietN.Components;
using FFHRRequestSystem.Services.VietN;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server;
using Blazored.Toast;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure SMTP settings
builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("Smtp"));

// Add services
builder.Services.AddScoped<TicketProcessingVietNService>();
builder.Services.AddScoped<ProcessingTypeVietNService>();
builder.Services.AddScoped<SystemUserAccountService>();
builder.Services.AddScoped<EmailService>();
builder.Services.AddScoped<ExcelExportService>();

// Add Blazored Toast
builder.Services.AddBlazoredToast();

// Add authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "BlazorAuth";
        options.LoginPath = "/account/login";
        options.AccessDeniedPath = "/account/forbidden";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();
builder.Services.AddCascadingAuthenticationState();

// Add HttpContext accessor
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
    app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
