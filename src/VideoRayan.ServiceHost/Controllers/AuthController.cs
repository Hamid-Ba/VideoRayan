using Framework.Api;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.CustomerAgg;
using VideoRayan.Application.Contract.CustomerAgg.Contracts;

namespace VideoRayan.ServiceHost.Controllers
{
    public class AuthController : ApiBaseController
    {
        private readonly ICustomerApplication _customerApplication;

        public AuthController(ICustomerApplication customerApplication) => _customerApplication = customerApplication;

        [HttpPost("Login")]
        public async Task<IActionResult> LoginOrRegister([FromBody] LoginCustomerDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _customerApplication.LoginFirstStep(command);
                    return result.IsSucceeded ? Ok(result.Message) : Problem(result.Message);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException?.Message); }
        }

        [HttpPost("Verify")]
        public async Task<IActionResult> GetTokenByVerification([FromBody] AccessTokenDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _customerApplication.VerifyLoginRegister(command);
                    return result.Item1.IsSucceeded ? Ok(new
                    {
                        Message = result.Item1.Message,
                        Token = result.Item2
                    })
                    :
                    Problem(result.Item1.Message);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.Message); }

        }
    }
}