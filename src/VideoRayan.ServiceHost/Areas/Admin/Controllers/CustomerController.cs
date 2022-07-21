using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.CustomerAgg.Contracts;

namespace VideoRayan.ServiceHost.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerApplication _customerApplication;

        public CustomerController(ICustomerApplication customerApplication) => _customerApplication = customerApplication;

        public IActionResult Index() => View();
        
    }
}
