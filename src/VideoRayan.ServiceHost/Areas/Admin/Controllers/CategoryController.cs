using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.CustomerAgg.Contracts;
using VideoRayan.Application.Contract.MeetingAgg.Contracts;

namespace VideoRayan.ServiceHost.Areas.Admin.Controllers
{
    public class CategoryController : AdminBaseController
    {
        private readonly ICategoryApplication _categoryApplication;
        private readonly ICustomerApplication _customerApplication;

        public CategoryController(ICategoryApplication categoryApplication, ICustomerApplication customerApplication)
        {
            _categoryApplication = categoryApplication;
            _customerApplication = customerApplication;
        }

        public async Task<IActionResult> Index(Guid customerId)
        {
            ViewBag.CustomerId = customerId;
            ViewBag.CustomerPhone = await _customerApplication.GetPhone(customerId);
            return View(await _categoryApplication.GetAll(customerId));
        }
    }
}