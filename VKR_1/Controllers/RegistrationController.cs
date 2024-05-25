using DAL.EfStructures;
using DAL.Repos.Interfaces;
using DAL.Repos;
using Microsoft.AspNetCore.Authorization;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using VKR_1.Models.Account;
using VKR_1.Models.Home;
using Microsoft.AspNetCore.Mvc.Rendering;
using Azure.Core;
using System.Drawing;
using VKR_1.Models.Registration;

namespace VKR_1.Controllers
{
    [Authorize]
    public class RegistrationController:BaseController
    {
        private readonly IUserRepo _userRepo;
        private readonly IRegistrationRepo _registerRepo;
        private readonly IRequestRepo _requestRepo;
        private readonly IRoomRepo _roomRepo;

        public RegistrationController(ApplicationDBContext context)
        {
            _userRepo = new UserRepo(context);
            _registerRepo = new RegistrationRepo(context);
            _requestRepo = new RequestRepo(context);
            _roomRepo = new RoomRepo(context);
        }

        public async Task<IActionResult> IndexAsync(long? RequestId)
        {  
            if (RequestId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            User? currentUser = await _userRepo.FindAsync(CurrentUserId);
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

            var request = await _requestRepo.FindAsync(RequestId);

            var floors = await _roomRepo.FindFloorsWithFreeRoomsByAdminAndGender(CurrentUserId, request.User.Gender);

            return View("Index", new RegistrationViewModel
            {
                CurrentRequest = request,
                Floors =  floors,    
            });
        }
        public async Task<IActionResult> GetRoomNumbers(string floor, string requestId)
        {
            if (floor == null || requestId == null)
            {
                return null;
            }
            var request = await _requestRepo.FindAsync(Convert.ToInt64(requestId));
            var numbers = await _roomRepo.FindNumbersWithFreeRoomsByAdminAndFloor(CurrentUserId, request.User.Gender, Convert.ToInt16(floor));
            return Json(numbers);
        }
        public async Task<IActionResult> GetRooms(string floor, string roomNumber ,string requestId)
        {
            if (floor == null || requestId == null ||roomNumber==null)
            {
                return null;
            }
            var request = await _requestRepo.FindAsync(Convert.ToInt64(requestId));
            var rooms = await _roomRepo.FindRoomsWithFreeRoomsByAdminAndFloorAndNumber(CurrentUserId, request.User.Gender, Convert.ToInt16(floor), Convert.ToInt16(roomNumber));
            return Json(rooms);
        }
        public async Task<IActionResult> RegisterAsync(RegistrationViewModel model)
        {
            if(model.SelectedFloor== null || model.SelectedRoomNumber==null|| model.SelectedRoom== null)
            {
                return RedirectToAction("Index");
            }

            await _registerRepo.AddAsync(new Registration
            {
                RequestId = model.CurrentRequest.Id,
                AdministratorId = CurrentUserId,
                Date = DateTime.Now,
                RoomId = (long)model.SelectedRoom,
            });
            return RedirectToAction("Index");
        }
    }

    
}
