using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UserRegistration.Application.IRepository;
using UserRegistration.Application.Models;
using UserRegistration.Data.Models;
using UserRegistration.Presentation.Helper;
using UserRegistration.Presentation.Models;

namespace UserRegistration.Presentation.Controllers
{
    
    public class UserController : Controller
    {
        private IRepowrapper _repoWrapper;
        public UserController(IRepowrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        [Authorize(Roles = "Admin, Customer")]
        public IActionResult Index()
        {
            var users = _repoWrapper.User.GetAllUsers();
            return View(users);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateUser()
        {
            var roleList = new List<SelectListItem>();
            roleList.Add(new SelectListItem() { Text = "Admin", Value = "1" });
            roleList.Add(new SelectListItem() { Text = "Customer", Value = "2" });
            ViewBag.Roles = roleList;
            var model = new UserCreateModel();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateUser(UserCreateModel model)
        {
            if (ModelState.IsValid)
            {
                Users user = new Users();
                user.UserName = model.UserName;
                user.RoleId = model.RoleId;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.CreatedBy = User.Identity.Name;
                user.CreatedDate = DateTime.Now;

                var userSalt = CryptoService.GenerateSalt();
                byte[] bytePassword = Encoding.UTF8.GetBytes(model.Password);
                var hmac = CryptoService.ComputeHMAC256(bytePassword, userSalt);

                user.Password = Convert.ToBase64String(hmac);
                user.PasswordSalt = Convert.ToBase64String(userSalt);

                _repoWrapper.User.Create(user);
                _repoWrapper.User.CommitSave();

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Entered User model is not valid");
                return View(model);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            var user = _repoWrapper.User.FindByCondition(x => x.UserId == Guid.Parse(id)).FirstOrDefault();
            if (user != null)
            {
                var model = new UserEditModel()
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email,
                    RoleId = user.RoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                var roleList = new List<SelectListItem>();
                roleList.Add(new SelectListItem() { Text = "Admin", Value = "1" });
                roleList.Add(new SelectListItem() { Text = "Customer", Value = "2" });
                ViewBag.Roles = roleList;
                return View(model);
            }
            ModelState.AddModelError("", "User does not exists.");
            return View(new UserEditModel());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(UserEditModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _repoWrapper.User.FindByCondition(x => x.UserId == model.UserId).FirstOrDefault();
                if (user != null)
                {
                    user.RoleId = model.RoleId;
                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    _repoWrapper.User.Update(user);
                    _repoWrapper.User.CommitSave();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "User does not exists.");
                    return View(model);
                }
            }
            ModelState.AddModelError("", "Invalid Model state");
            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var user = _repoWrapper.User.FindByCondition(x => x.UserId == Guid.Parse(id)).FirstOrDefault();
            if (user != null)
            {
                var model = new UserEditModel()
                {
                    UserId = user.UserId,
                    UserName = user.UserName,
                    Email = user.Email,
                    RoleId = user.RoleId,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                var roleList = new List<SelectListItem>();
                roleList.Add(new SelectListItem() { Text = "Admin", Value = "1" });
                roleList.Add(new SelectListItem() { Text = "Customer", Value = "2" });
                ViewBag.Roles = roleList;
                return View(model);
            }
            ModelState.AddModelError("", "User does not exists.");
            return View(new UserEditModel());
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(string id)
        {
            try
            {
                var user = _repoWrapper.User.FindByCondition(x => x.UserId == Guid.Parse(id)).FirstOrDefault();
                if (user != null)
                {
                    _repoWrapper.User.Delete(user);
                    _repoWrapper.User.CommitSave();

                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "User does not exists.");
                    return View(new UserEditModel());
                }
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "User does not exists.");
                return View(new UserEditModel());
            }
                           
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Details(string id)
        {
            try
            {
                var user = _repoWrapper.User.GetAllUsers().Where(x => x.UserId == Guid.Parse(id)).FirstOrDefault();
                return View(user);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = "Error! User information cannot be obtained.";
                return View(new UserBusinessModel());
            }
        }
        [Authorize(Roles = "Admin, Customer")]
        [HttpGet]
        public IActionResult Detail()
        {
            try
            {
                var user = _repoWrapper.User.GetAllUsers().Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                return View("Details", user);

            }
            catch (Exception ex)
            {
                ViewBag.ErrorMsg = "Error! User information cannot be obtained.";
                return View("Details", new UserBusinessModel());
            }
        }

    }
}