using DAL.EfStructures;
using DAL.Repos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VKR_1.Models.Account;
using VKR_1.Models.HomeAdmin;
using VKR_1.Models.Registration;

namespace VKR_1.Controllers
{
    [Authorize]
    public class HomeAdminController : BaseController
    {
        private readonly ApplicationDBContext _context;
        private readonly UserRepo _userRepo;
        private readonly GeneralRepo _generalRepo;
        private readonly TimeSlotRepo _timeSlotRepo;
        private readonly RegistrationRepo _registerRepo;
        private readonly RequestRepo _requsetRepo;
        public HomeAdminController(ApplicationDBContext context)
        {
            _context = context;
            _userRepo = new UserRepo(context);
            _generalRepo = new GeneralRepo(context);
            _timeSlotRepo = new TimeSlotRepo(context);
            _registerRepo = new RegistrationRepo(context);
            _requsetRepo = new RequestRepo(context);
        }

        public async Task<IActionResult> IndexAsync()
        {

            var currentUser = await _userRepo.FindAsync(CurrentUserId);
            if (currentUser == null)
            {
                return View("Index", new AccountViewModel
                {

                });
            }
            if (currentUser.Admin[0] == 0)
            {
                return RedirectToAction("Index", "Home");
            }

            var general = await _generalRepo.FindSingleAsync();
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
                Requests = await _requsetRepo.GetRequestsWithoutRegistrationByAdminAsync(CurrentUserId),
                Admins = await GetAdminsAsync(),
                StartDate = start,
                EndDate = end,
                DatesSet = set
            });


        }

        public async Task<IActionResult> CreateGeneralAsync(HomeViewModelAdmin model)
        {
    
            await _generalRepo.AddAsync(new General
            {
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Active = [1],

            });

            await CreateTimeSlots();

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RegisterAsync(RegistrationViewModel model)
        {
            if (ModelState["Room"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
            //if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            //var request = Req
            await _registerRepo.AddAsync(new Registration
            {
                RequestId = model.CurrentRequest.Id,
                AdministratorId = CurrentUserId,
                Date = DateTime.Now,
                Room = model.Room
            });
            return RedirectToAction("Index");
        }
        //private async Task<IEnumerable<Request>> GetRequestsAsync()
        //{
        //    return await _context.Requests
        //        .Include(r => r.User)
        //        .ToListAsync();
        //}
        private async Task<IEnumerable<User>> GetAdminsAsync()
        {
            return await _context.Users
                .Where(x => x.Admin[0] == 1)
                .ToListAsync();
        }

        

        private async ValueTask<int> CreateTimeSlots()
        {
            var admins = await _userRepo.FindAllAdminsAsync();
            var general = await _generalRepo.FindSingleAsync();
            List<TimeSlot> timeSlots = new List<TimeSlot>();

            foreach (var admin in admins)
            {
                foreach (DateTime day in EachDay(general.StartDate, general.EndDate))
                {
                    foreach (DateTime time in Each15Min(day))
                    {
                        timeSlots.Add(CreateTimeSlot(admin.Id, time));
                    }
                }
            }
            return await _timeSlotRepo.AddRangeAsync(timeSlots);
        }
        private TimeSlot CreateTimeSlot(long adminId, DateTime time)
        {
            return new TimeSlot()
            {
                Date = time,
                Free = [1],
                AdministratorId = adminId
            };
        }
        public IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        public IEnumerable<DateTime> Each15Min(DateTime date)
        {          
            var from = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0);
            var thru = new DateTime(date.Year, date.Month, date.Day, 18, 0, 0);

            for (var day = from; day < thru; day = day.AddMinutes(15))
                yield return day;
        }
    }
}
