using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Agenda.MVC.API;
using Agenda.MVC.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace Agenda.MVC.Areas.Login.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpService _httpService;

        public LoginController(HttpService httpService)
        {
            _httpService = httpService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(LoginViewModel loginViewModel)
        {
            var token = await _httpService.LoginAsync(loginViewModel);

            if (token == null)
            {
                ModelState.AddModelError(string.Empty, "Invalid username or password!");
                return View(loginViewModel);
            }

            await LoginCookie(new ClaimsIdentity(
                DecodeToken(token),
                CookieAuthenticationDefaults.AuthenticationScheme,
                "name",
                "role"));

            return Redirect(Url.Action("Index", "Home"));
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Redirect(Url.Action("Index"));
        }

        private IEnumerable<Claim> DecodeToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadJwtToken(token);
            var list = new List<Claim>(){new Claim("jwt-token", token)};
            list.AddRange(decodedToken.Claims);
            return list;
        }

        private async Task LoginCookie(ClaimsIdentity claimsIdentity)
        {
            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.
                // NUNCA MAIOR DO QUE O TEMPO DO TOKEN
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(99),
                // The time at which the authentication ticket expires. A
                // value set here overrides the ExpireTimeSpan option of
                // CookieAuthenticationOptions set with AddCookie.

                IsPersistent = true,
                // Whether the authentication session is persisted across
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                RedirectUri = "/"
                // The full path or absolute URI to be used as an http
                // redirect response value.
            };

            await HttpContext.SignInAsync(
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
