using Organic.Areas.Client.ViewModels.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Organic.Database;
using Organic.Services.Abstracts;
using Microsoft.AspNetCore.Identity;

namespace Organic.Controllers
{
    [Area("client")]
    [Route("auth")]
    public class AuthenticationController : Controller
    {
        private readonly DataContext _dbContext;
        private readonly IUserService _userService;

        public AuthenticationController(DataContext dbContext, IUserService userService)
        {
            _dbContext = dbContext;
            _userService = userService;
        }

        #region Login and Logout

        [HttpGet("login", Name = "client-auth-login")]
        public async Task<IActionResult> LoginAsync()
        {
            if (_userService.IsAuthenticated) return RedirectToRoute("client-home-index");

            return View(new LoginViewModel());
        }

        [HttpPost("login", Name = "client-auth-login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel? model)
        {
            if (!ModelState.IsValid) return View(model);

            if (!await _userService.CheckPasswordAsync(model!.Email, model!.Password))
            {
                ModelState.AddModelError(String.Empty, "Email or password is not correct");
                return View(model);
            }

            if (!await _userService.CheckEmailConfirmedAsync(model!.Email))
            {
                ModelState.AddModelError(String.Empty, "Email is not confirmed");
                return View(model);
            }

            await _userService.SignInAsync(model!.Email, model!.Password);
            return RedirectToRoute("client-home-index");
        }

        [HttpGet("logout", Name = "client-auth-logout")]
        public async Task<IActionResult> LogoutAsync()
        {
            await _userService.SignOutAsync();
            return RedirectToRoute("client-home-index");
        }

        #endregion

        #region Register

        [HttpGet("register", Name = "client-auth-register")]
        public ViewResult Register()

        {
            return View(new RegisterViewModel());
        }

        [HttpPost("register", Name = "client-auth-register")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            if (await _userService.CheckEmail(model.Email!))
            {
                ModelState.AddModelError("", "Bu email qeydiyyatdan keçib. Başqa email seçin.");
                return View(model);
            }

            await _userService.CreateAsync(model);
            return RedirectToRoute("client-auth-login");
        }

        #endregion

        #region ForgotPassword

        [HttpGet("forgot", Name ="client-forgot-password")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost("forgot", Name = "client-forgot-password")]
        public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordViewModel model)
        {
            if (!await _userService.CheckEmailandUserAsync(model!.Email))
            {
                ModelState.AddModelError(String.Empty, "Email is not confirmed");
                return View(model);
            }

            return RedirectToAction(nameof(EmailSendMessage));
        }

        [HttpGet("message", Name = "Email-Send-Message")]
        public async Task<ActionResult> EmailSendMessage()
        {
            return View();
        }

        [HttpGet("password/{token}", Name = "client-auth-password")]
        public async Task<IActionResult> PasswordActivate(string token)
        {
            return View(new NewPasswordViewModel());
        }

        [HttpPost("password/{token}", Name = "client-auth-password")]
        public async Task<IActionResult> PasswordActivate([FromRoute] string token, NewPasswordViewModel model)
        {
            if (!ModelState.IsValid) return View(model);
            var userActivation = await _dbContext.UserActivations
                .Include(ua => ua.User)
                .FirstOrDefaultAsync(ua => ua.User!.IsEmailConfirmed && ua.ActivationToken == token);

            if (userActivation is null) return NotFound();

            if (DateTime.Now > userActivation!.ExpireDate) return Ok("Token expired olub teessufler");

            userActivation!.User!.Password = model.Password;
            await _dbContext.SaveChangesAsync();
            return RedirectToRoute("client-auth-login");
        }

        [HttpGet("activate/{token}", Name = "client-auth-activate")]
        public async Task<IActionResult> ActivateAsync([FromRoute] string token)
        {
            var userActivation = await _dbContext.UserActivations
                .Include(ua => ua.User)
                .FirstOrDefaultAsync(ua =>
                    !ua!.User!.IsEmailConfirmed &&
                    ua.ActivationToken == token);

            if (userActivation is null) NotFound();

            if (DateTime.Now > userActivation!.ExpireDate) return Ok("Token expired olub teessufler");

            userActivation!.User!.IsEmailConfirmed = true;
            await _dbContext.SaveChangesAsync();
            return RedirectToRoute("client-auth-login");
        }

        #endregion
    }
}
