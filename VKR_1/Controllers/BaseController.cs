using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VKR_1.Controllers
{
    public abstract class BaseController : Controller
    {
        protected int CurrentUserId => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
    }
}
