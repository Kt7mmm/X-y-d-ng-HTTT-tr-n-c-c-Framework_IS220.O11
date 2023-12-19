using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers.Admin
{
    public class UploadController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
