using Microsoft.AspNetCore.Mvc;

namespace VideoRayan.ServiceHost.Areas.Admin.Controllers
{
    public class AudienceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
