using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Diagnostics;
using IdentityProject.Models;

namespace Cinema.Controllers
{
   
    public class HomeController : Controller
    {
        [Authorize(Roles = "User")]
        public IActionResult UserIndex()
        {
            return View("userDashboard");

        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminIndex()
        {
            //return View("adminDashboard");
            return View("~/Views/Admin/Main/Index.cshtml");
        }

        
    }
}
