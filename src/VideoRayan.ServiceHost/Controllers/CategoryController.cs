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

        /// <summary>
        /// همه گروه های یک مشتری را برمیگرداند
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("getAll/{customerId}")]
        public async Task<IActionResult> GetAll(Guid customerId)
        {
            try
            {
                var result = await _categoryApplication.GetAll(customerId);
                return Ok(result);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        /// تنها یک گروه خاص را برمیگرداند
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// برای ایجاد گروه استفاده می شود
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryApplication.Create(command);
                    return result.Item1.IsSucceeded ? Ok(new {message = result.Item1.Message,value = result.Item2 }) : BadRequest(result.Item1.Message);
                }
                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        /// برای ویرایش گروه استفاده می شود
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Edit")]
        public async Task<IActionResult> Edit([FromBody] EditCategoryDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _categoryApplication.Edit(command);
                    return result.Item1.IsSucceeded ? Ok(new { message = result.Item1.Message, value = result.Item2 }) : BadRequest(result.Item1.Message);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        /// برای حذف گروه استفاده می شود
        /// </summary>
        /// <returns></returns>

        [HttpPost("Delete/{id}/{customerId}")]
        public async Task<IActionResult> Delete(Guid id, Guid customerId)
        {
            try
            {
                var result = await _categoryApplication.Delete(id, customerId);
                return result.Item1.IsSucceeded ? Ok(new { message = result.Item1.Message, value = result.Item2 }) : BadRequest(result.Item1.Message);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }
    }
}