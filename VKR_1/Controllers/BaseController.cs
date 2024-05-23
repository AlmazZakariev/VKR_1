using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace VKR_1.Controllers
{
    public abstract class BaseController : Controller
    {
        protected long CurrentUserId
        {
            get
            {
                return long.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            }
        }
    }
}
