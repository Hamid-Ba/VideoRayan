using Framework.Api;
using Microsoft.AspNetCore.Authorization;
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

        /// <summary>
        /// مرحله اول ورود یا ثبت نام
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        [AllowAnonymous]
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

        /// <summary>
        /// مرحله دوم ورود یا ثبت نام (احراز هویت)
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("Verify")]
        [AllowAnonymous]
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
                        Id = result.Item2,
                        Token = result.Item3
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