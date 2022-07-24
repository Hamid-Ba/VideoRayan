using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.CustomerAgg.Contracts;

namespace VideoRayan.ServiceHost.Areas.Admin.Controllers
{
    public class AudienceController : AdminBaseController
    {
        private readonly ICustomerApplication _customerApplication;
        private readonly IAudienceApplication _audienceApplication;

        public AudienceController(ICustomerApplication customerApplication, IAudienceApplication audienceApplication)
        {
            _customerApplication = customerApplication;
            _audienceApplication = audienceApplication;
        }

        public async Task<IActionResult>  Index(Guid customerId)
        {
            ViewBag.CustomerId = customerId;
            ViewBag.CustomerPhone = await _customerApplication.GetPhone(customerId);
            return View(await _audienceApplication.GetAll(customerId,""));
        }
    }
}