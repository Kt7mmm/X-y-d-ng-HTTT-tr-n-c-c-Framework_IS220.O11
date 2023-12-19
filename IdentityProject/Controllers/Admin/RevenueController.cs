using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers.Admin
{
    public class RevenueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
