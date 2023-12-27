using IdentityProject.Context;
using IdentityProject.Models;
using IdentityProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Controllers.Admin
{
    [Authorize]
    public class SlotController : Controller
    {
        private readonly ISlotRepository _SlotRepository;
        private readonly CinemaDbContext _context;
        public SlotController(ISlotRepository SlotRepository, CinemaDbContext context)
        {
            _SlotRepository = SlotRepository;
            _context = context;
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {

            ViewData["Title"] = "Danh sách suất chiếu";
            ViewData["Slots"] = await _SlotRepository.GetAll();

            ViewData["Movies"] = _context.Movies.OrderBy(p => p.mv_name).ToList();

            return View("~/Views/Admin/Slot/List.cshtml");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Title"] = "Thêm suất chiếu";

            ViewData["Rooms"] = _context.Rooms.OrderBy(p => p.r_id).ToList();

            ViewData["Movies"] = _context.Movies.OrderBy(p => p.mv_name).ToList();

            return View("~/Views/Admin/Slot/Add.cshtml");
        }
        [HttpPost]
        public IActionResult Add(Slot slot)
        {
            var chosenMovie = _context.Movies.Where(p => p.mv_id == slot.mv_id).First<Movie>();
            slot.sl_duration = chosenMovie.mv_duration;
            slot.sl_end = slot.sl_start + chosenMovie.mv_duration;

            bool result = _SlotRepository.Create(slot);


            return RedirectToAction("List", "Slot", new { area = "" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string slotid, string roomid, string movieid)
        {
            Slot slot = await _SlotRepository.GetSlot(slotid, roomid, movieid);

            ViewData["Title"] = "Chỉnh sửa suất chiếu";

            ViewData["Rooms"] = _context.Rooms.OrderBy(p => p.r_id).ToList();

            ViewData["Movies"] = _context.Movies.OrderBy(p => p.mv_name).ToList();

            return View("~/Views/Admin/Slot/Edit.cshtml", slot);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Slot modifiedData)
        {
            Console.WriteLine("1" + modifiedData.sl_id);
            Console.WriteLine("2" + modifiedData.r_id);
            Console.WriteLine("3" + modifiedData.mv_id);
            Slot slot = await _SlotRepository.GetSlot(modifiedData.sl_id, modifiedData.r_id, modifiedData.mv_id);

            slot.sl_start = modifiedData.sl_start;
            slot.sl_price = modifiedData.sl_price;

            var chosenMovie = _context.Movies.Where(p => p.mv_id == modifiedData.mv_id).First<Movie>();
            slot.sl_duration = chosenMovie.mv_duration;
            slot.sl_end = modifiedData.sl_start + chosenMovie.mv_duration;


            bool result = _SlotRepository.Update(slot);

            ViewData["Title"] = "Danh sách suất chiếu";

            return RedirectToAction("List", "Slot", new { area = "" });
        }

        public IActionResult Delete(string slotid, string roomid, string movieid)
        {
            bool result = _SlotRepository.Destroy(slotid, roomid, movieid);

            ViewData["Title"] = "Danh sách suất chiếu";

            return RedirectToAction("List", "Slot", new { area = "" });
        }
    }
}
