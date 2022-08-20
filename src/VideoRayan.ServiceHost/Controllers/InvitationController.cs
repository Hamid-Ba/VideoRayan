using Framework.Api;
using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Application.Contract.MeetingAgg.Contracts;

namespace VideoRayan.ServiceHost.Controllers
{
    public class InvitationController : ApiBaseController
    {
        private readonly IMeetingApplication _meetingApplication;
        private readonly IAudienceMeetingApplication _audienceMeetingApplication;

        public InvitationController(IMeetingApplication meetingApplication, IAudienceMeetingApplication audienceMeetingApplication)
        {
            _meetingApplication = meetingApplication;
            _audienceMeetingApplication = audienceMeetingApplication;
        }

        /// <summary>
        /// سرویس مربوط به افزودن مخاطبان به کنفرانس
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InviteToMeeting([FromBody] AudienceMeetingDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _audienceMeetingApplication.AddAudiencesToMeeting(command);

                    if (result.IsSucceeded)
                    {
                        var setHostResult = await _meetingApplication.SetHost(command.MeetingId,command.HostId);
                        return setHostResult.IsSucceeded ? Ok(setHostResult.Message) : BadRequest(setHostResult.Message);
                    }

                    return Problem(result.Message);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }
    }
}