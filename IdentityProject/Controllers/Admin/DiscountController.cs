using IdentityProject.Models;
using IdentityProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Controllers.Admin
{
    public class DiscountController : Controller
    {
        private readonly IDiscountRepository _DiscountRepository;
        public DiscountController(IDiscountRepository DiscountRepository)
        {
            _DiscountRepository = DiscountRepository;
        }
        
        public async Task<IActionResult> List()
        {

            ViewData["Title"] = "Danh sách chương trình khuyến mãi";
            ViewData["Discounts"] = await _DiscountRepository.GetAll();

            return View("~/Views/Admin/Discount/List.cshtml");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Title"] = "Thêm chương trình khuyến mãi";

            return View("~/Views/Admin/Discount/Add.cshtml");
        }
        [HttpPost]
        public IActionResult Add(Discount discount)
        {
            bool result = _DiscountRepository.Create(discount);


            return RedirectToAction("List", "Discount", new { area = "" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            Discount discount = await _DiscountRepository.GetDiscount(id);

            ViewData["Title"] = "Chỉnh sửa chương trình khuyến mãi";

            return View("~/Views/Admin/Discount/Edit.cshtml", discount);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Discount modifiedData)
        {
            Discount discount = await _DiscountRepository.GetDiscount(modifiedData.dis_id);

            discount.dis_name = modifiedData.dis_name;
            discount.dis_start = modifiedData.dis_start;
            discount.dis_end = modifiedData.dis_end;
            discount.dis_percent = modifiedData.dis_percent;

            bool result = _DiscountRepository.Update(discount);

            ViewData["Title"] = "Danh sách chương trình khuyến mãi";

            return RedirectToAction("List", "Discount", new { area = "" });
        }

        public IActionResult Delete(string id)
        {
            bool result = _DiscountRepository.Destroy(id);

            ViewData["Title"] = "Danh sách chương trình khuyến mãi";

            return RedirectToAction("List", "Discount", new { area = "" });
        }
    }
}
