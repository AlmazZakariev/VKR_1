using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace VKR_1.ViewComponents
{
    public class RegistrationItemViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
