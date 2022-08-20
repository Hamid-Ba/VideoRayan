using Framework.Api;
using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Application.Contract.MeetingAgg.Contracts;

namespace VideoRayan.ServiceHost.Controllers
{
    public class MeetingController : ApiBaseController
    {
        private readonly IMeetingApplication _meetingApplication;

        public MeetingController(IMeetingApplication meetingApplication) => _meetingApplication = meetingApplication;

        /// <summary>
        /// دریافت همه کنفرانس های مشتری
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromQuery]FilterMeeting filter)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _meetingApplication.GetAllMeetingPaginated(filter);
                    return Ok(result);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        /// دریافت کنفرانس به خصوص
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var result = await _meetingApplication.GetBy(id);
                return Ok(result);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        /// دریافت مخاطبان دعوت شده به جلسه
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("audiences/{id}")]
        public async Task<IActionResult> GetAudiences(Guid id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _meetingApplication.GetAllBy(id);
                    return Ok(result);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        /// سرویس مربوط به ایجاد کنفرانس
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMeetingDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _meetingApplication.Create(command);
                    return result.IsSucceeded ? Ok(result.Message) : BadRequest(result.Message);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        /// سرویس مربوط به ویرایش کنفرانس
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody] EditMeetingDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _meetingApplication.Edit(command);
                    return result.IsSucceeded ? Ok(result.Message) : BadRequest(result.Message);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        /// سرویس مربوط به حذف کنفرانس
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete/{customerId}/{id}")]
        public async Task<IActionResult> Delete(Guid customerId, Guid id)
        {
            try
            {
                var result = await _meetingApplication.Delete(customerId, id);
                return result.IsSucceeded ? Ok(result.Message) : BadRequest(result.Message);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

    }
}