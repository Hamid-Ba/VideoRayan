using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.PlanAgg.Contracts;

namespace VideoRayan.ServiceHost.Controllers
{
    public class PlanController : ApiBaseController
    {
        private readonly IPlanApplication _planApplication;

        public PlanController(IPlanApplication planApplication) => _planApplication = planApplication;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var plans = await _planApplication.GetAll();
                return Ok(plans);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var plan = await _planApplication.GetPlanBy(id);
                return Ok(plan);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }
    }
}