using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.PlanAgg;
using VideoRayan.Application.Contract.PlanAgg.Contracts;

namespace VideoRayan.ServiceHost.Areas.Admin.Controllers
{
    public class PlanController : AdminBaseController
    {
        private readonly IPlanApplication _planApplication;

        public PlanController(IPlanApplication planApplication) => _planApplication = planApplication;

        public async Task<IActionResult> Index() => View(await _planApplication.GetAll());

        [HttpGet]
        public IActionResult Create() => PartialView();

        [HttpPost]
        public async Task<IActionResult> Create(CreatePlanVM command)
        {
            var result = await _planApplication.Create(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) => PartialView(await _planApplication.GetDetailForEditBy(id));

        [HttpPost]
        public async Task<IActionResult> Edit(EditPlanVM command)
        {
            var result = await _planApplication.Edit(command);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

        [HttpGet]
        //[PermissionChecker(MarketerPermissions.DeleteRole)]
        public IActionResult Delete(Guid id) => PartialView(id);

        [ActionName("Delete")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostDelete(Guid id)
        {
            var result = await _planApplication.Delete(id);

            if (result.IsSucceeded) TempData[SuccessMessage] = result.Message;

            return new JsonResult(result);
        }

    }
}