using IdentityProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Controllers.Admin
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _CustomerRepository;
        public CustomerController(ICustomerRepository CustomerRepository)
        {
            _CustomerRepository = CustomerRepository;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {

            ViewData["Title"] = "Danh sách khách hàng";
            ViewData["Customers"] = await _CustomerRepository.GetAll();

            return View("~/Views/Admin/Customer/List.cshtml");
        }

        public IActionResult Delete(string id)
        {
            bool result = _CustomerRepository.Destroy(id);

            ViewData["Title"] = "Danh sách khách hàng";

            return RedirectToAction("List", "Customer", new { area = "" });
        }
    }
}
