using Framework.Application.Enums;
using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.CustomerAgg.Contracts;

namespace VideoRayan.ServiceHost.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerApplication _customerApplication;

        public CustomerController(ICustomerApplication customerApplication) => _customerApplication = customerApplication;

        public async Task<IActionResult> Index(CustomerType type)
        {
            var result = await _customerApplication.GetAll(type);
            ViewBag.Type = type;
            return View(result);
        }
    }
}