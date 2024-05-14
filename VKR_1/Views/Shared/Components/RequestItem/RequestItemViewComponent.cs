using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace VKR_1.Views.Shared.Components.RequestItem
{
    public class RequestItemViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke(Request requst)
        {
            return View(requst);
        }
    }
}
