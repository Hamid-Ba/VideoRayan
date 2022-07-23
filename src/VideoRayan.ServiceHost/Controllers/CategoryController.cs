using Framework.Api;
using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Application.Contract.MeetingAgg.Contracts;

namespace VideoRayan.ServiceHost.Controllers
{
    public class CategoryController : ApiBaseController
    {
        private readonly ICategoryApplication _categoryApplication;

        public CategoryController(ICategoryApplication categoryApplication) => _categoryApplication = categoryApplication;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var result = await _categoryApplication.GetAll();
                return Ok(result);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBy(Guid id)
        {
            try
            {
                var result = await _categoryApplication.GetBy(id);
                return result is null ? NotFound(ApiResultMessages.NotFound) : Ok(result);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryApplication.Create(command);
                    return result.IsSucceeded ? Ok(result.Message) : BadRequest(result.Message);
                }
                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditCategoryDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryApplication.Edit(command);
                    return result.IsSucceeded ? Ok(result.Message) : BadRequest(result.Message);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        [HttpDelete("{id}/{customerId}")]
        public async Task<IActionResult> Delete(Guid id, Guid customerId)
        {
            try
            {
                var result = await _categoryApplication.Delete(id, customerId);
                return result.IsSucceeded ? Ok(result.Message) : BadRequest(result.Message);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }
    }
}