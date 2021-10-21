using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Models.DataModels;
using ViewModels.Authentication;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace FivemDocumentBlog.Areas.Account.Controllers
{
    [Area("Account")]
    public class IdentityController : Controller
    {

        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public IdentityController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        #region Register User

        [Route("Register")]
        public IActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserViewModel model)
        {

            if (!ModelState.IsValid) return View(model);

            var isExists = _userManager.Users.Any(u => u.NormalizedEmail == model.Email.ToUpper()
                                        || u.NormalizedUserName == model.Username.ToUpper());

            if (isExists)
            {
                ModelState.AddModelError("Username", "Username Or Email Is Exists");
                return View();
            }

            var user = new AppUser
            {
                UserName = model.Username,
                Email = model.Email,
                DisplayName = model.Username,
                ImageName = "default.png",
            };

            await _userManager.CreateAsync(user, model.Password);
            return RedirectToAction("Login");
        }

        #endregion

        #region Login User

        [Route("Login")]
        public IActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel model, string returnUrl = "/")
        {
            if (!ModelState.IsValid) return View(model);

            var user = _userManager.Users.FirstOrDefault(u => u.NormalizedEmail == model.UsernameOrEmail.ToUpper()
                                                       || u.NormalizedUserName == model.UsernameOrEmail.ToUpper());
            if (user == null)
            {
                ModelState.AddModelError("UsernameOrEmail", "Credential Is Invalid");
                return View(model);
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordCheck)
            {
                ModelState.AddModelError("UsernameOrEmail", "Credential Is Invalid");
                return View(model);
            }

            if (_signInManager.Options.SignIn.RequireConfirmedEmail ||
                _signInManager.Options.SignIn.RequireConfirmedEmail)
            {
                if (!user.EmailConfirmed)
                {
                    ModelState.AddModelError("UsernameOrEmail", "Please Confirm Your Email First");
                    return View(model);
                }
            }

            var tryLog = await _signInManager.PasswordSignInAsync(user, model.Password, true, false);

            if (tryLog.Succeeded)
            {
                return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : "/");
            }

            if (tryLog.IsLockedOut)
            {
                ModelState.AddModelError("UsernameOrEmail", "اکانت شما مسدود شده است لطفا بعدا تلاش کنید");
                return View(model);
            } else if (tryLog.IsNotAllowed)
            {
                ModelState.AddModelError("UsernameOrEmail", "شما مجاز به ورود نیستید ، لطفا رمز عبور خود را بررسی کنید درصورت صحیح بودن با مدیر سایت صحبت کنید");
                return View(model);
            } else if (tryLog.RequiresTwoFactor)
            {
                ModelState.AddModelError("UsernameOrEmail", "شما چطور تایید دومرحله ای گداشتی وقتی ما نداریم ؟؟؟؟");
                return View(model);
            }

            return View(model);
        }

        #endregion
    }
}
