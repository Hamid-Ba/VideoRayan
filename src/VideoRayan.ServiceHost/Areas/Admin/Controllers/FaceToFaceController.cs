using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.CustomerAgg.Contracts;
using VideoRayan.Application.Contract.MeetingAgg.Contracts;

namespace VideoRayan.ServiceHost.Areas.Admin.Controllers
{
    public class FaceToFaceController : AdminBaseController
    {
        private readonly IFaceToFaceApplication _faceToFaceApplication;
        private readonly ICustomerApplication _customerApplication;

        public FaceToFaceController(IFaceToFaceApplication faceToFaceApplication, ICustomerApplication customerApplication)
        {
            _faceToFaceApplication = faceToFaceApplication;
            _customerApplication = customerApplication;
        }

        public async Task<IActionResult> Index(Guid customerId)
        {
            ViewBag.CustomerId = customerId;
            ViewBag.CustomerPhone = await _customerApplication.GetPhone(customerId);
            var type = await _customerApplication.GetTypeBy(customerId);
            return View(await _faceToFaceApplication.GetAll(customerId));
        }

        public async Task<IActionResult> Detail(Guid id) => PartialView(await _faceToFaceApplication.GetBy(id));

        public async Task<IActionResult> Participants(Guid id) => View(await _faceToFaceApplication.GetAllBy(id));
    }
}
