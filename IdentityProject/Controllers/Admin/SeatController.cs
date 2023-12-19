using IdentityProject.Context;
using IdentityProject.Models;
using IdentityProject.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IdentityProject.Controllers.Admin
{
    public class SeatController : Controller
    {
        private readonly ISeatRepository _SeatRepository;
        private readonly CinemaDbContext _context;
        public SeatController(ISeatRepository SeatRepository, CinemaDbContext context)
        {
            _SeatRepository = SeatRepository;
            _context = context;
        }

        public async Task<IActionResult> List()
        {

            ViewData["Title"] = "Danh sách ghế ngồi";
            ViewData["Seats"] = await _SeatRepository.GetAll();

            return View("~/Views/Admin/Seat/List.cshtml");
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["Title"] = "Thêm vị trí ngồi";

            ViewData["Rooms"] = _context.Rooms.OrderBy(p => p.r_id).ToList();

            return View("~/Views/Admin/Seat/Add.cshtml");
        }
        [HttpPost]
        public IActionResult Add(Seat Seat)
        {
            bool result = _SeatRepository.Create(Seat);


            return RedirectToAction("List", "Seat", new { area = "" });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string seatid, string roomid)
        {
            Seat Seat = await _SeatRepository.GetSeat(seatid, roomid);

            ViewData["Title"] = "Chỉnh sửa vị trí ngồi";

            ViewData["Rooms"] = _context.Rooms.OrderBy(p => p.r_id).ToList();

            return View("~/Views/Admin/Seat/Edit.cshtml", Seat);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Seat modifiedData)
        {
            Seat seat = await _SeatRepository.GetSeat(modifiedData.st_id, modifiedData.r_id);

            seat.st_type = modifiedData.st_type;

            bool result = _SeatRepository.Update(seat);

            ViewData["Title"] = "Danh sách ghế ngồi";

            return RedirectToAction("List", "Seat", new { area = "" });
        }

        public IActionResult Delete(string seatid, string roomid)
        {
            bool result = _SeatRepository.Destroy(seatid, roomid);

            ViewData["Title"] = "Danh sách ghế ngồi";

            return RedirectToAction("List", "Seat", new { area = "" });
        }
    }
}
