using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Framework.Application.Authentication
{
    public class AuthHelper : IAuthHelper
    {
        private readonly IHttpContextAccessor _httpContext;

        public AuthHelper(IHttpContextAccessor httpContext) => _httpContext = httpContext;

        public async void SignInAsync(VisitorAuthViewModel account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Role, "Visitor"),
                new Claim(ClaimTypes.Name, account.Fullname!),
                new Claim(ClaimTypes.MobilePhone, account.Mobile!),
                new Claim("Code", account.Code!),

            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties()
            {
                IsPersistent = true
            };


           await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(claimsIdentity), authProperties);
        }

        public async void SignInAsync(OperatorAuthViewModel account)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                new Claim(ClaimTypes.Role, "Operator"),
                new Claim("RoleId", account.RoleId.ToString()),
                new Claim("RoleName", account.RoleName!.ToString()),
                new Claim(ClaimTypes.Name, account.Fullname!),
                new Claim(ClaimTypes.MobilePhone, account.Mobile!),

            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new Microsoft.AspNetCore.Authentication.AuthenticationProperties()
            {
                IsPersistent = true
            };


            await _httpContext.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
               new ClaimsPrincipal(claimsIdentity), authProperties);
        }
        public async void SignOut() => await _httpContext.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    }
}