using DAL.Repos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using VKR_1.Models.Registration;

namespace VKR_1.ViewComponents
{
    public class RequestItemViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(RegistrationViewModel registration)
        {
            return View(registration);
        }
    }
}
