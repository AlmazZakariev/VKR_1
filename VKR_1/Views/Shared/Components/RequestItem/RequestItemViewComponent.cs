using DAL.Repos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace VKR_1.Views.Shared.Components.RequestItem
{
    public class RequestItemViewComponent : ViewComponent
    {
        private readonly RequestRepo _requestRepo;
        public RequestItemViewComponent(RequestRepo requestRepo)
        {
            _requestRepo = requestRepo;
        }
        public IViewComponentResult Invoke()
        {
            var requests =  _requestRepo.GetAll();
            return View(requests);
        }
    }
}
