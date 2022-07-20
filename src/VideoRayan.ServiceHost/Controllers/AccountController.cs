using Framework.Application.Authentication;
using Microsoft.AspNetCore.Mvc;
using VideoRayan.Application.Contract.AccountAgg;
using VideoRayan.Application.Contract.AccountAgg.Contracts;

namespace VideoRayan.ServiceHost.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthHelper _authHelper;
        private readonly IOperatorApplication _operatorApplication;

        protected string ErrorMessage = "ErrorMessage";
        protected string SuccessMessage = "SuccessMessage";
        protected string InfoMessage = "InfoMessage";
        protected string WarningMessage = "WarningMessage";

        public AccountController(IAuthHelper authHelper, IOperatorApplication operatorApplication)
        {
            _authHelper = authHelper;
            _operatorApplication = operatorApplication;
        }

        #region Admin User

        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginOperatorVM command)
        {
            if (ModelState.IsValid)
            {
                var result = await _operatorApplication.Login(command);

                if (result.IsSucceeded)
                {
                    TempData[SuccessMessage] = result.Message;
                    return RedirectToAction("Index", "Home", new { Area = "Admin" });
                }

                TempData[ErrorMessage] = result.Message;
            }

            return View(command);
        }

        #endregion

        [HttpGet]
        public IActionResult Logout()
        {
            _authHelper.SignOut();
            TempData[SuccessMessage] = "با موفقیت خارج شدید";
            return RedirectToAction("Login", "Account");
        }
    }
}