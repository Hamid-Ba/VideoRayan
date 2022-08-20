using Framework.Api;
using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.MeetingAgg;
using VideoRayan.Application.Contract.MeetingAgg.Contracts;

namespace VideoRayan.ServiceHost.Controllers
{
    public class FaceToFaceController : ApiBaseController
    {
        private readonly IFaceToFaceApplication _faceToFaceApplication;

        public FaceToFaceController(IFaceToFaceApplication faceToFaceApplication) => _faceToFaceApplication = faceToFaceApplication;

        /// <summary>
        /// دریافت همه کنفرانس های حضوری مشتری
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        [HttpGet("all/{customerId}")]
        public async Task<IActionResult> GetAll(Guid customerId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _faceToFaceApplication.GetAll(customerId);
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
                var result = await _faceToFaceApplication.GetBy(id);
                return Ok(result);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        ///  دریافت مخاطبان دعوت شده به جلسه حضوری
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
                    var result = await _faceToFaceApplication.GetAllBy(id);
                    return Ok(result);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        /// سرویس مربوط به ایجاد کنفرانس حضوری
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateFaceToFaceDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _faceToFaceApplication.Create(command);
                    return result.IsSucceeded ? Ok(result.Message) : BadRequest(result.Message);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        /// سرویس مربوط به ویرایش کنفرانس حضوری
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("edit")]
        public async Task<IActionResult> Edit([FromBody] EditFaceToFaceDto command)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _faceToFaceApplication.Edit(command);
                    return result.IsSucceeded ? Ok(result.Message) : BadRequest(result.Message);
                }

                return BadRequest(ApiResultMessages.ModelStateNotValid);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }

        /// <summary>
        /// سرویس مربوط به حذف کنفرانس حضوری
        /// </summary>
        /// <returns></returns>
        [HttpPost("delete/{customerId}/{id}")]
        public async Task<IActionResult> Delete(Guid customerId, Guid id)
        {
            try
            {
                var result = await _faceToFaceApplication.Delete(customerId, id);
                return result.IsSucceeded ? Ok(result.Message) : BadRequest(result.Message);
            }
            catch (Exception e) { return BadRequest(e.InnerException!.Message); }
        }
    }
}