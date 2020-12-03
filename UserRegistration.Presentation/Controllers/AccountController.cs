using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserRegistration.Application.IRepository;
using UserRegistration.Data.Models;
using UserRegistration.Presentation.Helper;
using UserRegistration.Presentation.Models;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace UserRegistration.Presentation.Controllers
{
    public class AccountController : Controller
    {
        private IRepowrapper _repoWrapper;
        public AccountController(IRepowrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        [AllowAnonymous]
        public IActionResult UserNameExists(string userName)
        {
            bool isUsernameExists = _repoWrapper.User.FindByCondition(x => x.UserName == userName).Any();
            return Json(!isUsernameExists);
        }

        public IActionResult EmailExists(string email)
        {
            bool isEmailExists = _repoWrapper.User.FindByCondition(m => m.Email == email).Any();
            return Json(!isEmailExists);
        }

        [HttpGet]
        public IActionResult Register()
        {
            CustomerRegistrationViewModel model = new CustomerRegistrationViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Register(CustomerRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users();
                user.UserName = model.UserName;
                user.Email = model.Email;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;

                var userSalt = CryptoService.GenerateSalt();
                byte[] bytePassword = Encoding.UTF8.GetBytes(model.Password);
                var hmac = CryptoService.ComputeHMAC256(bytePassword, userSalt);

                user.Password = Convert.ToBase64String(hmac);
                user.PasswordSalt = Convert.ToBase64String(userSalt);
                user.RoleId = 2;
                user.CreatedBy = model.UserName;
                user.CreatedDate = DateTime.Now;
                _repoWrapper.User.Create(user);
                _repoWrapper.User.CommitSave();

                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Message = "Some Errors still there.";
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginViewModel model = new LoginViewModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!_repoWrapper.User.FindByCondition(x=>x.UserName == model.UserName).Any())
            {
                ModelState.AddModelError("", "Username is not valid");
                return View(model);
            }
            
            Users user = _repoWrapper.User.FindByCondition(m => m.UserName == model.UserName).Single();
            //Get salt from db and convert to byte array
            byte[] salt = Convert.FromBase64String(user.PasswordSalt);

            //Get password entered by user and convert to byte array
            byte[] bytePassword = Encoding.UTF8.GetBytes(model.Password);

            //hash the password entered by user on login screen using salt coming from db
            byte[] passwordConversion = CryptoService.ComputeHMAC256(bytePassword, salt);

            //converting passwordConversion to string
            string actualPassword = Convert.ToBase64String(passwordConversion);

            if (user.Password != actualPassword)
            {
                ModelState.AddModelError("", "Username and Password is invalid.");
                return View(model);
            }

            var role = _repoWrapper.User.GetUserRole(user.RoleId);

            ClaimsIdentity identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserName),
                 new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, role)
            }, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if(role == "Customer")
            {
                return RedirectToAction(actionName: "Index", controllerName: "Customer");
            }
           return RedirectToAction(actionName: "Index", controllerName: "User");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Unauthorized()
        {
            return View();
        }

        public IActionResult PageNotFound()
        {
            return View();
        }
    }
}