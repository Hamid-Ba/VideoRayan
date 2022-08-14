using Framework.Api;
using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Application.Contract.MeetingAgg.Contracts;

namespace VideoRayan.ServiceHost.Controllers
{
    public class InvitationController : ApiBaseController
    {
        private readonly IAudienceMeetingApplication _audienceMeetingApplication;

        public InvitationController(IAudienceMeetingApplication audienceMeetingApplication) => _audienceMeetingApplication = audienceMeetingApplication;


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
                    return result.IsSucceeded ? Ok(result.Message) : BadRequest(result.Message);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }
    }
}