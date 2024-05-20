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
using VKR_1.Views.Home;

namespace VKR_1.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly UserRepo _userRepo;
        private readonly RequestRepo _requestRepo;
        private readonly GeneralRepo _generalRepo;
        public HomeController(ApplicationDBContext context)
        {
            _userRepo = new UserRepo(context);
            _requestRepo = new RequestRepo(context);
            _generalRepo= new GeneralRepo(context);
        }

        public async Task<IActionResult> IndexAsync()

        {
            Request? request = await _requestRepo.FindByUserAsync(CurrentUserId);
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
            var general = await _generalRepo.FindSingleAsync();

            if (general != null)
            {

                //if  (!(model.PreferenceDate.Date >= general.StartDate.Date && model.PreferenceDate.Date <= general.EndDate.Date))
                //{
                //    //пустая дата
                //    //model.PreferenceDate = null;
                //} 
            }
            else
            {
                return View("_ShowRequest", new HomeViewModel()
                {

                });
            }
            if (!ModelState.IsValid)
            {
                model.Request = await _requestRepo.FindByUserAsync(CurrentUserId);
                return View("Index", model);
            }

            await _requestRepo.AddAsync(new Request
            {
                Date = DateTime.Now,
                PreferenceDate = model.PreferenceDate,
                UserId = CurrentUserId
            });         
            return RedirectToAction("Index");
        }

        //private async Task<Request?> GetRequestCurrentUserAsync()
        //{
        //    return await _context.Requests
        //        .Include(r => r.User)
        //        .FirstOrDefaultAsync(x => x.UserId == CurrentUserId);

        //}

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
