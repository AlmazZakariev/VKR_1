using DAL.EfStructures;
using DAL.Repos;
using DAL.Repos.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VKR_1.Models.Account;
using VKR_1.Models.Home;

namespace VKR_1.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly IUserRepo _userRepo;
        private readonly IRequestRepo _requestRepo;
        private readonly IGeneralRepo _generalRepo;
        public HomeController(ApplicationDBContext context)
        {
            _userRepo = new UserRepo(context);
            _requestRepo = new RequestRepo(context);
            _generalRepo = new GeneralRepo(context);
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

        public async Task<IActionResult> CreateAsync(HomeViewModel model)
        {
            var general = await _generalRepo.FindSingleAsync();

            if (general == null)
            {
                return View("_CreateRequest", new HomeViewModel()
                {

                });
            }

            //if (!ModelState.IsValid)
            //{
            //    model.Request = await _requestRepo.FindByUserAsync(CurrentUserId);
            //    return View("Index", model);
            //}

            await _requestRepo.AddAsync(new Request
            {
                Date = DateTime.Now,
                PreferenceDate = model.PreferenceDate,
                UserId = CurrentUserId
            });
            return RedirectToAction("Index");
        }  
    }
}
