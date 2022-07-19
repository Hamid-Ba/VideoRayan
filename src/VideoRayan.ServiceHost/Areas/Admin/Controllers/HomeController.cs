using Microsoft.AspNetCore.Mvc;

namespace VideoRayan.ServiceHost.Areas.Admin.Controllers
{
    public class HomeController : AdminBaseController
    {
        public IActionResult Index() => View();
    }
}