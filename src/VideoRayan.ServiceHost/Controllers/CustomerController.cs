using Framework.Api;
using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.CustomerAgg;
using VideoRayan.Application.Contract.CustomerAgg.Contracts;

namespace VideoRayan.ServiceHost.Controllers
{
    public class CustomerController : ApiBaseController
    {
        private readonly ICustomerApplication _customerApplication;

        public CustomerController(ICustomerApplication customerApplication) => _customerApplication = customerApplication;

        /// <summary>
        /// دریافت مشتری به خصوص
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBy(Guid id)
        {
            try
            {
                var result = await _customerApplication.GetBy(id);
                return Ok(result);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        /// برای ویرایش مشتری استفاده می شود
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody]EditCustomerDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _customerApplication.Edit(command);
                    return result.Item1.IsSucceeded ? Ok(new { message = result.Item1.Message, value = result.Item2 }) : BadRequest(result.Item1.Message);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        /// برای ویرایش لوگو مشتری استفاده می شود
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("editLogo")]
        public async Task<IActionResult> EditLogo([FromForm] EditLogoCustomerDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _customerApplication.EditLogo(command);
                    return result.Item1.IsSucceeded ? Ok(new { message = result.Item1.Message, value = result.Item2 }) : BadRequest(result.Item1.Message);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        /// برای ویرایش تصویر مشتری استفاده می شود
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("editImage")]
        public async Task<IActionResult> EditImage([FromForm] EditLogoCustomerDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _customerApplication.EditImage(command);
                    return result.Item1.IsSucceeded ? Ok(new { message = result.Item1.Message, value = result.Item2 }) : BadRequest(result.Item1.Message);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        /// دریافت جلسات
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("invitedMeetings/{id}")]
        public async Task<IActionResult> GetInvitedMeetings(Guid id)
        {
            try
            {
                var result = await _customerApplication.CustomerMeetingList(id);
                return Ok(result);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }
    }
}