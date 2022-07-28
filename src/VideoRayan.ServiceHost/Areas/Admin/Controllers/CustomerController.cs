using Framework.Application.Enums;
using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.CustomerAgg;
using VideoRayan.Application.Contract.CustomerAgg.Contracts;

namespace VideoRayan.ServiceHost.Areas.Admin.Controllers
{
    public class CustomerController : AdminBaseController
    {
        private readonly ICustomerApplication _customerApplication;

        public CustomerController(ICustomerApplication customerApplication) => _customerApplication = customerApplication;

        public async Task<IActionResult> Index(CustomerType type)
        {
            var result = await _customerApplication.GetAll(type);
            ViewBag.Type = type;
            return View(result);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(Guid id) => PartialView(await _customerApplication.GetBy(id));

        [HttpGet]
        public IActionResult Create() => PartialView("Create");

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerDto command)
        {
            var result = await _customerApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) => PartialView(await _customerApplication.GetDetailForEditByAdmin(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditByAdminCustomerDto command)
        {
            var result = await _customerApplication.Edit(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> ChangeStatus(Guid id)
        {
            var result = await _customerApplication.ActiveOrDeactive(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SendMessage(Guid id) => PartialView(new SendSmsCustomerDto() { Id = id });

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendMessage(SendSmsCustomerDto command)
        {
            var result = await _customerApplication.SendMessage(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public IActionResult Delete(Guid id) => PartialView(id);

        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(Guid id)
        {
            var result = await _customerApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
            return new JsonResult(result);
        }
    }
}