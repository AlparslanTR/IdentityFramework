using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityFrameworkWepApp.Controllers
{
    public class OrderController : Controller
    {
        [Authorize(Policy ="OrderPermission")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
