using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rain33.Models;
using System.Security.Cryptography;

namespace Rain33.Controllers
{
    public class AccountController : Controller
    {
        //private NoviceContext db = new NoviceContext();
        private readonly NoviceContext db;

        private string sha(string rawString)
        {
            SHA256 sha256 = new SHA256Managed();
            byte[] sha256Bytes = System.Text.Encoding.UTF8.GetBytes(rawString);
            byte[] cryString = sha256.ComputeHash(sha256Bytes);
            string sha256Str = string.Empty;
            for (int i = 0; i < cryString.Length; i++)
            {
                sha256Str += cryString[i].ToString();
            }
            return sha256Str;
            //return System.Convert.ToBase64String(cryString);
        }

        public AccountController(NoviceContext context)
        {
            db = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string userName, string password)
        {
            if (!string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }

            //Check the user name and password  
            //Here can be implemented checking logic from the database  
            ClaimsIdentity identity = null;
            bool isAuthenticated = false;

            // Admin
            if (userName != null && password != null)
            {
                var account = db.Uporabnik.SingleOrDefault(a => a.username.Equals(userName) && a.password.Equals(password) && a.Roles.Equals(true));

                if (account != null)
                {
                    //Create the identity for the user  
                    identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, "Admin")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    isAuthenticated = true;
                }
            }

            // User
            if (userName != null && password != null)
            {
                var account = db.Uporabnik.SingleOrDefault(a => a.username.Equals(sha(userName)) && a.password.Equals(sha(password)) && a.Roles.Equals(false));

                if (account != null)
                {
                    //Create the identity for the user  
                    identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, userName),
                    new Claim(ClaimTypes.Role, "User")
                    }, CookieAuthenticationDefaults.AuthenticationScheme);

                    isAuthenticated = true;
                }
                
            }

            if (isAuthenticated)
            {
                var principal = new ClaimsPrincipal(identity);

                var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}