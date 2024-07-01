using System.Diagnostics.Metrics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using WebApplication17.Data;
using WebApplication17.Models.Products;
using WebApplication17.Models.Users;

namespace WebApplication17.Controllers
{
    public class AccountController : Controller
    {
        private readonly HomeDb entiti;
        private readonly ILogger<AccountController> logger;
        private readonly IMemoryCache cache;
        public AccountController(ILogger<AccountController> logger, HomeDb entiti, IMemoryCache cache)
        {
            this.logger = logger;
            this.entiti = entiti;
            this.cache = cache;
        }
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLogin login)
        {
            var user = await entiti.Users.Where(x => x.Email == login.Email && x.Password == login.Password).FirstOrDefaultAsync();
            if (user != null)
            {
                var claim = new List<Claim>() { new Claim(ClaimTypes.Name, user.Username), };

                var claimIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));

                logger.LogInformation("Entity Signed in!");
                return RedirectToAction("SucurePage");
            }
            else
            {
                if (login.Email != null && login.Password != null)
                {
                    ModelState.AddModelError("", "Email or Password is not correct!");
                    logger.LogError("Entity write wrong Email or Password!");


                    ViewBag.ErrorMessage = "Email or Password is not correct!";

                    return View();
                }
                return View();
            }
        }
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegister register)
        {
            if (ModelState.IsValid)
            {
                UserRegister user = new()
                {
                    Username = register.Username,
                    Email = register.Email,
                    Password = register.Password,
                    RepeatPassword = register.RepeatPassword
                };

                var item = await entiti.Users.AnyAsync(x => x.Email == user.Email && x.Password == user.Password);

                try
                {
                    if (item)
                    {
                        ModelState.AddModelError("", "This email already exist try new one!");
                        logger.LogError("Entity already exist!");

                        ViewBag.ErrorMessage = "Enter uniqe email or password!";
                        return View();
                    }
                    else
                    {
                        await entiti.Users.AddAsync(user);
                        await entiti.SaveChangesAsync();

                        logger.LogInformation("Entity Created!");
                        return RedirectToAction("Login");
                    }
                }
                catch (DbUpdateException ub)
                {
                    ModelState.AddModelError("", "Enter uniqe email or password!");
                    return BadRequest();
                }
            }
            return View();
        }
        [Authorize]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            logger.LogInformation("Entity Sign out!");
            return RedirectToAction("Register");
        }
        [Authorize]
        public async Task<IActionResult> SucurePage()
        {
            ViewBag.Name = HttpContext.User.Identity.Name;
            return View();
        }
        //[Authorize]
        //public async Task<IActionResult> ProductPage(Product product)
        //{
            
        //}
    }
}
