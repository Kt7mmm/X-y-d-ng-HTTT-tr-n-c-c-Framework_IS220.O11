using cinema.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace cinema.Controllers.Admin
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _AccountRepository;
        public AccountController(IAccountRepository AccountRepository)
        {
            _AccountRepository = AccountRepository;
        }

        public async Task<IActionResult> List()
        {

            ViewData["Title"] = "Danh sách tài khoản";
            ViewData["Accounts"] = await _AccountRepository.GetAll();

            return View("~/Views/Admin/Account/List.cshtml");
        }

        public IActionResult Delete(string id)
        {
            bool result = _AccountRepository.Destroy(id);

            ViewData["Title"] = "Danh sách tài khoản";

            return RedirectToAction("List", "Account", new { area = "" });
        }
    }
}
