using DAL.EfStructures;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Diagnostics;
using VKR_1.Models;
using VKR_1.Models.Home;

namespace VKR_1.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {


        private readonly ApplicationDBContext _context;

        public HomeController(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()

        {
            Request? request = GetRequestCurrentUserAsync().Result;
            if (request != null)
            {
                return View("_ShowRequest", new HomeViewModel{
                    
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
                .FirstOrDefaultAsync(x => x.UserId == CurrentUserId);
 
        }

    }
}
