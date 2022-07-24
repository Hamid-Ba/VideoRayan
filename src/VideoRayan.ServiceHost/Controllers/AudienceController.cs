using Framework.Api;
using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.CustomerAgg;
using VideoRayan.Application.Contract.CustomerAgg.Contracts;

namespace VideoRayan.ServiceHost.Controllers
{
    public class AudienceController : ApiBaseController
    {
        private readonly IAudienceApplication _audienceApplication;

        public AudienceController(IAudienceApplication audienceApplication) => _audienceApplication = audienceApplication;


        [HttpGet("{customerId}/{categoryName}")]
        public async Task<IActionResult> GetAll(Guid customerId, string categoryName)
        {
            try
            {
                var result = await _audienceApplication.GetAll(customerId, categoryName);
                return Ok(result);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var result = await _audienceApplication.GetBy(id);
                return Ok(result);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAudienceDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _audienceApplication.Create(command);
                    return result.IsSucceeded ? Ok(result.Message) : BadRequest(result.Message);
                }
                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        [HttpPut]
        public async Task<IActionResult> Edit([FromBody] EditAudienceDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _audienceApplication.Edit(command);
                    return result.IsSucceeded ? Ok(result.Message) : BadRequest(result.Message);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }
    }
}