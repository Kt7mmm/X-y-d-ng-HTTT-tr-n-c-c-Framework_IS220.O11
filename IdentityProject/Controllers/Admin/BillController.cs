using IdentityProject.Context;
using IdentityProject.Models;
using IdentityProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Controllers.Admin
{
    [Authorize]
    public class BillController : Controller
    {
        private readonly IBillRepository _BillRepository;
        private readonly CinemaDbContext _dbContext;

        public BillController(IBillRepository BillRepository, CinemaDbContext dbContext)
        {
            _BillRepository = BillRepository;
            _dbContext = dbContext;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {

            ViewData["Title"] = "Danh sách hóa đơn";
            ViewData["Bills"] = await _BillRepository.GetAll();
            ViewData["Customers"] = _dbContext.Customers.OrderBy(p => p.cus_name).ToList();

            return View("~/Views/Admin/Bill/List.cshtml");
        }

        public async Task<IActionResult> Detail(string id)
        {

            ViewData["Title"] = "Chi tiết hóa đơn";
            Bill bill = await _BillRepository.GetBill(id);
            ViewData["Customers"] = _dbContext.Customers.OrderBy(p => p.cus_name).ToList();
            ViewData["Tickets"] = _dbContext.Tickets.OrderBy(p => p.tk_id).Where(x => x.bi_id == id).ToList();
            ViewData["Discounts"] = _dbContext.Discounts.OrderBy(p => p.dis_id).ToList();
            ViewData["ChosenDiscounts"] = _dbContext.ApplyDiscounts.OrderBy(p => p.dis_id).Where(x => x.bi_id == id).ToList();

            return View("~/Views/Admin/Bill/Detail.cshtml", bill);
        }

        public IActionResult Delete(string id)
        {
            bool result = _BillRepository.Destroy(id);

            ViewData["Title"] = "Danh sách hóa đơn";

            return RedirectToAction("List", "Bill", new { area = "" });
        }
    }
}
