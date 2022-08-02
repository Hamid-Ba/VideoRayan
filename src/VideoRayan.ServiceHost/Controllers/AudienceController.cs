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
                    return result.Item1.IsSucceeded ? Ok(new { message = result.Item1.Message, value = result.Item2 }) : BadRequest(result.Item1.Message);
                }
                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody] EditAudienceDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _audienceApplication.Edit(command);
                    return result.Item1.IsSucceeded ? Ok(new { message = result.Item1.Message, value = result.Item2 }) : BadRequest(result.Item1.Message);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        [HttpPost("Delete/{id}/{customerId}")]
        public async Task<IActionResult> Delete(Guid id, Guid customerId)
        {
            try
            {
                var result = await _audienceApplication.Delete(id, customerId);
                return result.Item1.IsSucceeded ? Ok(new { message = result.Item1.Message, value = result.Item2 }) : BadRequest(result.Item1.Message);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }
    }
}