using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using IdentityProject.Models;

namespace Cinema.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //public IActionResult AdminIndex()
        //{
        //    return View("adminDashboard");
        //}
        //public IActionResult UserIndex()
        //{
        //    return View("userDashboard");
        //}
        public IActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                return RedirectToAction("AdminIndex");
            }
            else if (User.IsInRole("User"))
            {
                return RedirectToAction("UserIndex");
            }
            else
            {
                return View();
            }
        }

        public IActionResult AdminIndex()
        {
            return View("AdminIndex");
        }

        public IActionResult UserIndex()
        {
            return View("UserIndex");
        }
    }
}
