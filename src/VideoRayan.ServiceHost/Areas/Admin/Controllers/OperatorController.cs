using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using VideoRayan.Application.Contract.AccountAgg;
using VideoRayan.Application.Contract.AccountAgg.Contracts;

namespace VideoRayan.ServiceHost.Areas.Admin.Controllers
{
    public class OperatorController : AdminBaseController
    {
        private readonly IRoleApplication _roleApplication;
        private readonly IOperatorApplication _operatorApplication;

        public OperatorController(IRoleApplication roleApplication, IOperatorApplication operatorApplication)
        {
            _roleApplication = roleApplication;
            _operatorApplication = operatorApplication;
        }

        public async Task<IActionResult> Index() => View(await _operatorApplication.GetAll());

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = new SelectList(await _roleApplication.GetAll(), "Id", "Name");
            return PartialView("Create");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateOperatorVM command)
        {
            if (!ModelState.IsValid) ViewBag.Roles = new SelectList(await _roleApplication.GetAll(), "Id", "Name");

            var result = await _operatorApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            ViewBag.Roles = new SelectList(await _roleApplication.GetAll(), "Id", "Name");
            return PartialView(await _operatorApplication.GetDetailForEditBy(id));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditOperatorVM command)
        {
            if (!ModelState.IsValid) ViewBag.Roles = new SelectList(await _roleApplication.GetAll(), "Id", "Name");

            var result = await _operatorApplication.Edit(command);

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
            var result = await _operatorApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;
            return new JsonResult(result);
        }

    }
}