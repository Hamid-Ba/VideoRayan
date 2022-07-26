using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.CustomerAgg.Contracts;
using VideoRayan.Application.Contract.MeetingAgg.Contracts;

namespace VideoRayan.ServiceHost.Areas.Admin.Controllers
{
    public class MeetingController : AdminBaseController
    {
        private readonly IMeetingApplication _meetingApplication;
        private readonly ICustomerApplication _customerApplication;

        public MeetingController(IMeetingApplication meetingApplication, ICustomerApplication customerApplication)
        {
            _meetingApplication = meetingApplication;
            _customerApplication = customerApplication;
        }

        public async Task<IActionResult>  Index(Guid customerId)
        {
            ViewBag.CustomerId = customerId;
            ViewBag.CustomerPhone = await _customerApplication.GetPhone(customerId);
            var type = await _customerApplication.GetTypeBy(customerId);
            return View(await _meetingApplication.GetAll(customerId));
        }

        public async Task<IActionResult> Detail(Guid id) => PartialView(await _meetingApplication.GetBy(id));

        public async Task<IActionResult> Participants(Guid id) => View(await _meetingApplication.GetAllBy(id));
    }
}