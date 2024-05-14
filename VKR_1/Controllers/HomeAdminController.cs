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

            var requests = await GetRequestsAsync();
            return View("Index", new HomeViewModelAdmin
            {
                Requests = await GetRequestsAsync(),
                Admins = await GetAdminsAsync()
            });

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
    }
}
