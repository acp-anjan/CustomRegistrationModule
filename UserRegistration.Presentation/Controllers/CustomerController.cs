using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserRegistration.Application.IRepository;

namespace UserRegistration.Presentation.Controllers
{
    public class CustomerController : Controller
    {
        private IRepowrapper _repoWrapper;
        public CustomerController(IRepowrapper repoWrapper)
        {
            _repoWrapper = repoWrapper;
        }
        [Authorize(Roles = "Customer")]
        public IActionResult Index()
        {
            var username = User.Identity.Name;
            var user = _repoWrapper.User.FindByCondition(x => x.UserName == username).Single();
            ViewBag.FirstName = user.FirstName;
            ViewBag.LastName = user.LastName;
            return View();
        }
    }
}