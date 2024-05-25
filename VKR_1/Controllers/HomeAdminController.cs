using DAL.EfStructures;
using DAL.Repos;
using DAL.Repos.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;
using VKR_1.Models.Account;
using VKR_1.Models.HomeAdmin;
//using VKR_1.Models.Registration;

namespace VKR_1.Controllers
{
    [Authorize]
    public class HomeAdminController : BaseController
    {
        private readonly IUserRepo _userRepo;
        private readonly IGeneralRepo _generalRepo;
        private readonly ITimeSlotRepo _timeSlotRepo;
        private readonly IRegistrationRepo _registerRepo;
        private readonly IRequestRepo _requestRepo;
        private readonly IRoomRepo _roomRepo;
        public HomeAdminController(ApplicationDBContext context)
        { 
            _userRepo = new UserRepo(context);
            _generalRepo = new GeneralRepo(context);
            _timeSlotRepo = new TimeSlotRepo(context);
            _registerRepo = new RegistrationRepo(context);
            _requestRepo = new RequestRepo(context);
            _roomRepo = new RoomRepo(context);
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

            var requests = await _requestRepo.GetRequestsWithoutRegistrationByAdminAsync(CurrentUserId);
            List<RegistrationViewModel> registrations = new();
            foreach (var request in requests)
            {
                registrations.Add(new() { CurrentRequest = request });
            }

            return View("Index", new HomeViewModelAdmin
            {
                //Requests = await _requsetRepo.GetRequestsWithoutRegistrationByAdminAsync(CurrentUserId),
               
                Registrations = registrations,
                Admins = await _userRepo.FindAllAdminsAsync(),
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
        //public async Task<IActionResult> RegisterAsync(RegistrationViewModel model)
        //{
        //    if (ModelState["Room"].ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
        //    {
        //        return RedirectToAction("Index");
        //    }
            
        //    await _registerRepo.AddAsync(new Registration
        //    {
        //        RequestId = model.CurrentRequest.Id,
        //        AdministratorId = CurrentUserId,
        //        Date = DateTime.Now,
        //        Room = model.Room
        //    });
        //    return RedirectToAction("Index");
        //}
        public async Task<IActionResult> OpenRegistrationAsync(RegistrationViewModel model)
        {
            return RedirectToAction("Index", "Registration", new { requestId = model.CurrentRequest.Id });
        }
        //public async void LoadFloorsAsync(RegistrationViewModel model)
        //{
        //    //model.Floors = await _roomRepo.FindFloorsWithFreeRoomsByAdminAndGender(CurrentUserId, model.CurrentRequest.User.Gender);
        //}
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
        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }
        private IEnumerable<DateTime> Each15Min(DateTime date)
        {          
            var from = new DateTime(date.Year, date.Month, date.Day, 9, 0, 0);
            var thru = new DateTime(date.Year, date.Month, date.Day, 18, 0, 0);

            for (var day = from; day < thru; day = day.AddMinutes(15))
                yield return day;
        }
    }
}
