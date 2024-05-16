using DAL.EfStructures;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VKR_1.Models.HomeAdmin;

namespace VKR_1.Controllers
{
    public class HomeAdminController:BaseController
    {
        private readonly ApplicationDBContext _context;

        public HomeAdminController(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var general = GetGeneralAsync().Result;
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            bool set = false;
            if (general != null)
            {
                set = general.Active[0] == 1;
                start = general.StartDate;
                end = general.EndDate;
            }
           
            return View("Index", new HomeViewModelAdmin
            {
                    Requests = await GetRequestsAsync(),
                    Admins = await GetAdminsAsync(),
                    StartDate = start,
                    EndDate = end,
                    DatesSet = set
            });
            

        }

        public async Task<IActionResult> CreateGeneralAsync(HomeViewModelAdmin model)
        {

            await _context.General.AddAsync(new General
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Active = [1],

            });

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<Request>> GetRequestsAsync()
        {
            return await _context.Requests
                .Include(r => r.User)
                .ToListAsync();
        }
        private async Task<IEnumerable<User>> GetAdminsAsync()
        {
            return await _context.Users
                .Where(x => x.Admin[0] == 1)
                .ToListAsync();
        }

        private async Task<General?> GetGeneralAsync()
        {
            return await _context.General.FirstOrDefaultAsync(g => g.Id > 1);
        }
    }
}
