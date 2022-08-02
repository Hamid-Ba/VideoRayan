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

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetAll(Guid customerId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _meetingApplication.GetAll(customerId);
                    return Ok(result);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

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