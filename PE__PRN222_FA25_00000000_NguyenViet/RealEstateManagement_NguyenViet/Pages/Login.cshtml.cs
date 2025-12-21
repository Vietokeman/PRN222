using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using RealEstateManagement.Services;

namespace RealEstateManagement_NguyenViet.Pages
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SystemUserService _userAccountService;

        public LoginModel() => _userAccountService ??= new SystemUserService();

        [BindProperty]
        [EmailAddress]
        [Required]
        public string Username { get; set; } = string.Empty;

        [BindProperty]
        [Required]
        public string Password { get; set; } = string.Empty;


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {                        
            var userAccount = await _userAccountService.GetAccount(Username, Password);

            if (userAccount != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userAccount.Username),
                    new Claim(ClaimTypes.Role, userAccount.UserRole.ToString()),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                Response.Cookies.Append("Username", userAccount.Username);

                return RedirectToPage("/Contracts/Index");
            }
            else
            {
                TempData["Message"] = "Invalid Username or Password!";
            }

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Page();
        }
    }    
}
