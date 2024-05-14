using Domain.Entities;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using VKR_1.Models.Account;
using DAL.EfStructures;
using Microsoft.EntityFrameworkCore;
using DAL.Controllers;

namespace VKR_1.Controllers
{
    public class AccountController : BaseController
    {
        private readonly ApplicationDBContext _context;

        public AccountController(ApplicationDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> LoginAsync([Bind(Prefix = "l")] LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new AccountViewModel
                {
                    LoginViewModel = model
                });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);


            if (user is null)
            {
                ViewBag.Error = "Некорректные логин и(или) пароль";
                return View("Index", new AccountViewModel
                {
                    LoginViewModel = model
                });
            }
            else
            {
                var correctPass = SecretHasher.Verify(model.Password, user.Pass);
                if (!correctPass)
                {
                    ViewBag.Error = "Некорректные логин и(или) пароль";
                    return View("Index", new AccountViewModel
                    {
                        LoginViewModel = model
                    });
                }               
            }

            await AuthenticateAsync(user);
            return RedirectToAction("Index", RedirectToHomeString(user));
        }

        private async Task AuthenticateAsync(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
            };

            var id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> RegisterAsync([Bind(Prefix = "r")] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", new AccountViewModel
                {
                    RegisterViewModel = model
                });
            }

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
            if (user != null)
            {
                ViewBag.RegisterError = "Пользователь с таким логином уже существует!";
                return View("Index", new AccountViewModel
                {
                    RegisterViewModel = model
                });
            }

            user = new User{Name = model.Name, Surname = model.Surname, Patronymic = model.Patronymic, Email = model.Email, Phone = model.Phone, Pass = SecretHasher.Hash(model.Password), Admin = [0] };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            await AuthenticateAsync(user);
            return RedirectToAction("Index", RedirectToHomeString(user));
        }

        public async Task<IActionResult> LogoutAsync()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        private string RedirectToHomeString(User user)
        {
            if (user.Admin[0] == 0)
            {

                return "Home";
            }
            else
            {
                return "HomeAdmin";
            }
        }
    }
}
