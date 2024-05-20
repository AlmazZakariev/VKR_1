using DAL.EfStructures;
using DAL.Repos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Collections;
using System.Diagnostics;
using VKR_1.Models;
using VKR_1.Models.Account;
using VKR_1.Models.Home;

namespace VKR_1.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {


        private readonly ApplicationDBContext _context;
        private readonly UserRepo _userRepo;
        public HomeController(ApplicationDBContext context)
        {
            _context = context;
            _userRepo = new UserRepo(context);
        }

        public async Task<IActionResult> IndexAsync()

        {
            Request? request = await GetRequestCurrentUserAsync();
            User? currentUser = await _userRepo.FindAsync(CurrentUserId);
            if (currentUser == null)
            {
                return View("Index", new AccountViewModel
                {

                });
            }
            else
            {
                if (currentUser.Admin[0] == 1)
                {
                    return RedirectToAction("Index", "HomeAdmin");
                }

                if (request != null)
                {
                    return View("_ShowRequest", new HomeViewModel
                    {

                        Request = request

                    });
                }
                else
                {
                    return View("_CreateRequest", new HomeViewModel
                    {

                    });
                }
            }
            

        }


        public async Task<IActionResult> CreateAsync(HomeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Request = await GetRequestCurrentUserAsync();
                return View("Index", model);
            }

            await _context.Requests.AddAsync(new Request
            {
                Date = DateTime.Now,
                PreferenceDate = model.PreferenceDate,
                UserId = CurrentUserId
            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task<Request?> GetRequestCurrentUserAsync()
        {
            return await _context.Requests
                .Include(r => r.User)
                .FirstOrDefaultAsync(x => x.UserId == CurrentUserId);

        }

        //private string RedirectToHomeString(User user)
        //{
        //    if (user.Admin[0] == 0)
        //    {

        //        return "Home";
        //    }
        //    else
        //    {
        //        return "HomeAdmin";
        //    }
        //}
    }
}
