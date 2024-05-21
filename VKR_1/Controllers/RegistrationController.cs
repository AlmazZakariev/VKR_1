using DAL.EfStructures;
using DAL.Repos;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using VKR_1.Models.Account;
using VKR_1.Models.Registration;

namespace VKR_1.Controllers
{
    [Authorize]
    public class RegistrationController: BaseController
    {
        
        private readonly RequestRepo _requsetRepo;
        public RegistrationController(ApplicationDBContext context)
        {
            
            _requsetRepo = new RequestRepo(context);
        }
        //public async Task<IActionResult> IndexAsync(long reqId)
        //{
        //    var request = await _requsetRepo.FindAsync(reqId);

        //    return View("Index", new RegistrationViewModel
        //    {
        //        CurrentRequest = request,
        //    });
        //}
        public  IActionResult Index()
        {
            var request =  _requsetRepo.FindAsync(4).Result;

            return View("Index", new RegistrationViewModel
            {
                CurrentRequest = request,

            });
        }
    }
}
