using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using IdentityProject.Models;

namespace Cinema.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        //[HttpGet]
        //public IActionResult Index()
        //{
        //    Console.WriteLine("heyheyhey");
        //    if (User.IsInRole("Admin"))
        //    {
        //        //trả về view trong Views/Home/Shared/adminDash
        //        return View("~/Views/Home/Shared/adminDashboard.cshtml");
        //    }
        //    else if (User.IsInRole("User"))
        //    {
        //        return View("~/Views/Home/Shared/userDashboard.cshtml");
        //    }
        //    else
        //    {
        //        return View();
        //    }
        //}

        [Authorize(Roles = "Admin")]
        public IActionResult AdminIndex()
        {
            return View("adminDashboard");
        }

        [Authorize(Roles = "User")]
        public IActionResult UserIndex()
        {
            return View("UserIndex");
        }
    }
}
